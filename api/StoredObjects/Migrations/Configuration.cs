namespace StoredObjects.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StoredObjects.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StoredObjects.ApplicationContext context)
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

            context.Set<ShurahBasedOrganisation>().AddOrUpdate(x=>x.Name,mainOrganisation);

            context.SaveChanges();

            mainOrganisation = context.Set<ShurahBasedOrganisation>().First(x => x.Name == mainOrganisation.Name);
            var member = new Member
            {
                Introduction = "For some explanations of my views you can visit my personal blog http://investigatingIslam.org ",
                JoinedOnDateAndTimeUtc = theStartDate,
                Moderated = false,
                Removed = false,
                Organisation = mainOrganisation,
                OrganisationId = mainOrganisation.Id,
                LastDateAndTimeUtcAgreedToMembershipRules = theStartDate,
                PublicName = "Lamaan Ball",
                EmailAddress = "lamaan@lamaan.com"
            };
            context.Set<Member>().AddOrUpdate(x=>x.EmailAddress,member);

            context.SaveChanges();

            member = context.Set<Member>().First(x => x.PublicName == "Lamaan Ball");
            var user = new Auth0User {Id = "facebook|10154497931532170",Name = "Lamaan Ball",PictureUrl = "https://scontent.xx.fbcdn.net/v/t1.0-1/p50x50/1959327_10152239153037170_2068892341_n.jpg?oh=9197c2274a8a0b7c3f548ca6407da74e&oe=5884F154" };

            context.Set<Auth0User>().AddOrUpdate(x=>x.Id,user);
            context.SaveChanges();
            var memberAuthUser = new MemberAuth0User
            {
                Auth0UserId = user.Id,
                MemberId = member.Id
            };
            context.Set<MemberAuth0User>().AddOrUpdate(x=> new{x.Auth0UserId,x.MemberId},memberAuthUser);
            context.SaveChanges();

            context.Set<OrganisationLeader>().AddOrUpdate(x=>x.OrganisationId,new OrganisationLeader
            {
                LastUpdateDateTimeUtc = DateTime.UtcNow,
                Leader = member,
                Organisation = mainOrganisation,
                OrganisationId = mainOrganisation.Id,
                LeaderMemberId = member.Id
            });
            context.SaveChanges();
        }
    }
}
