using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
  public interface ISeederService
  {
    void EnsureSeedDataPresent();
  }
  public class SeederService : ISeederService
  {
    private ApplicationContext _context;
    public SeederService(ApplicationContext context)
    {
      _context = context;
    }
    public void EnsureSeedDataPresent()
    {
      var theStartDate = new DateTime(2016, 11, 6, 9, 51, 0, DateTimeKind.Utc);
     
      var mainOrganisation = new ShurahBasedOrganisation
      {
        Name = "ShariahStandards.org",
        Description = "For all Muslims who have an interest in developing a global open standard interpretation of Islamic law in English",
        JoiningPolicy = JoiningPolicy.NoApplicationNeeded,
        LastUpdateDateTimeUtc = theStartDate,
        UrlDomain = "https://shariahstandards.org",
        RequiredNumbersOfAcceptingMembers = 1
      };
      var existingMainOrganisation = _context.Set<ShurahBasedOrganisation>().FirstOrDefault(x => x.Name == mainOrganisation.Name);
      if (existingMainOrganisation == null)
      {
        _context.Set<ShurahBasedOrganisation>().Add(mainOrganisation);
        _context.SaveChanges();
      }
      existingMainOrganisation = _context.Set<ShurahBasedOrganisation>().FirstOrDefault(x => x.Name == mainOrganisation.Name); 

      var member = new Member
      {
        Introduction = "For some explanations of my views you can visit my personal blog http://investigatingIslam.org ",
        JoinedOnDateAndTimeUtc = theStartDate,
        Moderated = false,
        Removed = false,
        Organisation = existingMainOrganisation,
        OrganisationId = existingMainOrganisation.Id,
        LastDateAndTimeUtcAgreedToMembershipRules = theStartDate,
        PublicName = "Lamaan Ball",
        EmailAddress = "lamaan@lamaan.com"
      };
      var existingMember = _context.Set<Member>().FirstOrDefault(x => x.EmailAddress == "lamaan@lamaan.com");
      if (existingMember == null)
      {
        _context.Set<Member>().Add(member);
        _context.SaveChanges();
      }
      existingMember = _context.Set<Member>().FirstOrDefault(x => x.EmailAddress == "lamaan@lamaan.com");

      var user = new Auth0User { Id = "facebook|10154497931532170", Name = "Lamaan Ball", PictureUrl = "https://scontent.xx.fbcdn.net/v/t1.0-1/p50x50/1959327_10152239153037170_2068892341_n.jpg?oh=9197c2274a8a0b7c3f548ca6407da74e&oe=5884F154" };

      var existingUser = _context.Set<Auth0User>().FirstOrDefault(x => x.Id == user.Id);

      if (existingUser == null)
      {
        _context.Set<Auth0User>().Add(user);
        _context.SaveChanges();
      }
      existingUser = _context.Set<Auth0User>().FirstOrDefault(x => x.Id == user.Id);

      var memberAuthUser = new MemberAuth0User
      {
        Auth0UserId = existingUser.Id,
        MemberId = existingMember.Id
      };
      var existingMemberAuth0User = _context.Set<MemberAuth0User>().FirstOrDefault(x => x.MemberId == existingMember.Id
      && x.Auth0UserId == existingUser.Id);
      if (existingMemberAuth0User == null)
      {
        _context.Set<MemberAuth0User>().Add(memberAuthUser);
        _context.SaveChanges();
      }
      existingMemberAuth0User = _context.Set<MemberAuth0User>().FirstOrDefault
        (x => x.MemberId == existingMember.Id && x.Auth0UserId == existingUser.Id);

      var existingOrganisationLeader = existingMainOrganisation.OrganisationLeader;
      if (existingOrganisationLeader == null)
      {

        _context.Set<OrganisationLeader>().Add(new OrganisationLeader
        {
          LastUpdateDateTimeUtc = DateTime.UtcNow,
          Leader = existingMember,
          Organisation = existingMainOrganisation,
          OrganisationId = existingMainOrganisation.Id,
          LeaderMemberId = existingMember.Id
        });
        _context.SaveChanges();
      }
    }
  }
}
