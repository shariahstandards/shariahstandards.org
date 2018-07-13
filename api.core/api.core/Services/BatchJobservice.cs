using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using WebApiResources;

namespace Services
{
  public interface IBatchJobServiceDependencies
  {
    IStorageService StorageService { get; set; }
  }
  public class BatchJobServiceDependencies : IBatchJobServiceDependencies
  {
    public IStorageService StorageService { get; set; }

    public BatchJobServiceDependencies(IStorageService storageService)
    {
      StorageService = storageService;
    }
  }
  public interface IBatchJobService
  {
    BatchJobResultResource RunCounts();
  }
  public class BatchJobService : IBatchJobService
  {
    private readonly IBatchJobServiceDependencies _dependencies;

    public BatchJobService(IBatchJobServiceDependencies dependencies)
    {
      _dependencies = dependencies;
    }

    public BatchJobResultResource RunCounts()
    {
      var resource = new BatchJobResultResource();
      resource.OrganisationUpdates =
_dependencies.StorageService.SetOf<ShurahBasedOrganisation>().ToList().Select(RunOrganisationCounts).ToList();
      return resource;
    }

    private OrganisationUpdateResource RunOrganisationCounts(ShurahBasedOrganisation org)
    {

      org.CountingInProgress = true;
      _dependencies.StorageService.SaveChanges();
      var resource = SetLeader(org);
      org.CountingInProgress = false;
      _dependencies.StorageService.SaveChanges();
      return resource;
    }

    private OrganisationUpdateResource SetLeader(ShurahBasedOrganisation org)
    {
      var resource = new OrganisationUpdateResource();
      var level0leadership =
          _dependencies.StorageService.SetOf<LeaderRecognition>().Where(l =>
          l.Member.OrganisationId == org.Id
          && !l.Member.Removed
          && !l.RecognisedLeaderMember.Removed
          && !l.Member.Followers.Any()).ToList();

      level0leadership.ForEach(l =>
      {
        l.Member.FollowerCount = 0;
      });

      var level0LeaderRecognitionsGroups = level0leadership.GroupBy(l => l.RecognisedLeaderMemberId).ToList();
      level0LeaderRecognitionsGroups.ForEach(g =>
      {
        var leader = g.First().RecognisedLeaderMember;
        leader.FollowerCount = g.Count();
      });
      var leaderIds = level0LeaderRecognitionsGroups.Select(l => l.Key).ToList();

      while (leaderIds.Any())
      {
        var ids = leaderIds;
        var levelNLeadership =
            _dependencies.StorageService.SetOf<LeaderRecognition>().Where(l =>
            !l.Member.Removed
            && !l.RecognisedLeaderMember.Removed
            && l.Member.OrganisationId == org.Id
            && ids.Contains(l.MemberId)).ToList();

        var levelNLeadersGroups = levelNLeadership.GroupBy(l => l.RecognisedLeaderMemberId).ToList();
        levelNLeadersGroups.ForEach(g =>
        {
          var leader = g.First().RecognisedLeaderMember;
          leader.FollowerCount = g.Sum(f => f.Member.FollowerCount + 1);
        });

        leaderIds = levelNLeadersGroups.Select(g => g.Key).ToList();
      }
      _dependencies.StorageService.SaveChanges();

      if (org.OrganisationLeader?.LastUpdateDateTimeUtc.Date >= DateTime.UtcNow.Date)
      {
        SetOrganisationDetails(resource, org);
        return resource;
      }
      var leaderCandidate =
          org.Members.Where(m => !m.Removed).OrderByDescending(m => m.FollowerCount).FirstOrDefault();
      if (leaderCandidate != null && (leaderCandidate.FollowerCount + 1) > (org.Members.Count(m => !m.Removed) / 2.0))
      {
        resource.NewLeader = org?.OrganisationLeader.Leader.Id!=leaderCandidate.Id;

        if (org.OrganisationLeader == null)
        {
          org.OrganisationLeader = new OrganisationLeader();
          org.OrganisationLeader.OrganisationId = org.Id;
          org.OrganisationLeader.Organisation = org;
          _dependencies.StorageService.SetOf<OrganisationLeader>().Add(org.OrganisationLeader);
        }
        org.OrganisationLeader.Leader = leaderCandidate;
        org.OrganisationLeader.LeaderMemberId = leaderCandidate.Id;
        org.OrganisationLeader.LastUpdateDateTimeUtc = DateTime.UtcNow;
      }
      else if (org.OrganisationLeader != null)
      {
        org.OrganisationLeader.LastUpdateDateTimeUtc = DateTime.UtcNow;
      }
      SetOrganisationDetails(resource, org);
      _dependencies.StorageService.SaveChanges();
      return resource;
    }

    private void SetOrganisationDetails(OrganisationUpdateResource resource, ShurahBasedOrganisation org)
    {
      if (org == null) { return; }
      if (org.OrganisationLeader == null) { return; }
      resource.LeaderMemberId = org.OrganisationLeader.LeaderMemberId;
      resource.LeaderPublicName = org.OrganisationLeader.Leader.PublicName;
      resource.Name = org.Name;
      resource.OrganisationId = org.Id;
      resource.LeaderFollowerCount = org.OrganisationLeader.Leader.FollowerCount;
    }
  }
}
