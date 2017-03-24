using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using StoredObjects;

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
        void RunCounts(string key);
    }
    public class BatchJobService: IBatchJobService
    {
        private readonly IBatchJobServiceDependencies _dependencies;

        public BatchJobService(IBatchJobServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public void RunCounts(string key)
        {
            if (key != "lkjglyrelbztglbjflgyaedfevuvsglstflr") { return;}

            _dependencies.StorageService.SetOf<ShurahBasedOrganisation>().ToList().ForEach(RunOrganisationCounts);
        }

        private void RunOrganisationCounts(ShurahBasedOrganisation org)
        {
           
            org.CountingInProgress = true;
            _dependencies.StorageService.SaveChanges();
            SetLeader(org);
            org.CountingInProgress = false;
            _dependencies.StorageService.SaveChanges();

        }

        private void SetLeader(ShurahBasedOrganisation org)
        {
            var level0leadership =
                _dependencies.StorageService.SetOf<LeaderRecognition>().Where(l => 
                l.Member.OrganisationId==org.Id
                &&!l.Member.Removed
                &&!l.RecognisedLeaderMember.Removed
                &&!l.Member.Followers.Any()).ToList();

            level0leadership.ForEach(l =>
            {
                l.Member.FollowerCount = 0;
            });

            var level0LeadersGroups = level0leadership.GroupBy(l => l.RecognisedLeaderMemberId).ToList();
            level0LeadersGroups.ForEach(g =>
            {
                var leader = g.First().RecognisedLeaderMember;
                leader.FollowerCount = g.Count();
            });
            var leaderIds = level0LeadersGroups.Select(l => l.Key).ToList();

            while (leaderIds.Any())
            {
                var ids = leaderIds;
                var levelNLeadership =
                    _dependencies.StorageService.SetOf<LeaderRecognition>().Where(l => 
                    !l.Member.Removed
                    &&!l.RecognisedLeaderMember.Removed
                    && l.Member.OrganisationId==org.Id
                    && ids.Contains(l.MemberId)).ToList();

                var levelNLeadersGroups = levelNLeadership.GroupBy(l => l.RecognisedLeaderMemberId).ToList();
                levelNLeadersGroups.ForEach(g =>
                {
                    var leader = g.First().RecognisedLeaderMember;
                    leader.FollowerCount = g.Sum(f=>f.Member.FollowerCount+1);
                });

                leaderIds = levelNLeadersGroups.Select(g => g.Key).ToList();
            }
            _dependencies.StorageService.SaveChanges();

            if (org.OrganisationLeader?.LastUpdateDateTimeUtc.Date >= DateTime.UtcNow.Date)
            {
                return;
            }
            var leaderCandidate =
                org.Members.Where(m => !m.Removed).OrderByDescending(m => m.FollowerCount).FirstOrDefault();
            if (leaderCandidate != null && (leaderCandidate.FollowerCount +1) > (org.Members.Count(m => !m.Removed)/2.0))
            {
                if (org.OrganisationLeader == null)
                {
                    org.OrganisationLeader = _dependencies.StorageService.SetOf<OrganisationLeader>().Create();
                    org.OrganisationLeader.OrganisationId = org.Id;
                    org.OrganisationLeader.Organisation = org;
                    _dependencies.StorageService.SetOf<OrganisationLeader>().Add(org.OrganisationLeader);
                }
                org.OrganisationLeader.Leader = leaderCandidate;
                org.OrganisationLeader.LeaderMemberId = leaderCandidate.Id;
                org.OrganisationLeader.LastUpdateDateTimeUtc = DateTime.UtcNow;
            }
            else if(org.OrganisationLeader!=null)
            {
                org.OrganisationLeader.LastUpdateDateTimeUtc = DateTime.UtcNow;
            }
            _dependencies.StorageService.SaveChanges();
        }
    }
}
