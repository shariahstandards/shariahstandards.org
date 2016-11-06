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
            };

            context.Set<ShurahBasedOrganisation>().AddOrUpdate(x=>x.Name,mainOrganisation);

            context.SaveChanges();

            var member = new Member
            {
                Name = "Lamaan Ball",
                Email = "lamaan@lamaan.com",
                Introduction = "For some explanations of my views you can visit my personal blog http://investigatingIslam.org ",
                JoinedOnDateAndTimeUtc = theStartDate,
                Moderated = false,
                Removed = false,
                Organisation = mainOrganisation,
                LastDateAndTimeUtcAgreedToMembershipRules = theStartDate
            };
            context.Set<Member>().AddOrUpdate(x=>x.Email,member);

            context.SaveChanges();
        }
    }
}
