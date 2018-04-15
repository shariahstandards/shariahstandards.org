using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects
{
    public class Human
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        //public virtual Birth Birth { get; set; }
        //public virtual Death Death { get; set; }
        //public virtual MotherRelationship MotherRelationship { get; set; }
        //public virtual FatherRelationship FatherRelationship { get; set; }
        public DateTime RegistrationDateTimeUtc { get; set; }
        public virtual IList<Auth0User> Logins { get; set; }
        public virtual IList<Member> Memberships { get; set; }
        public virtual IList<MembershipApplication> MembershipApplications { get; set; }
        public virtual IList<ContactDetail> ContactDetails { get; set; }
        public virtual IList<MembershipRuleSectionAcceptance> MembershipRuleSectionAcceptances { get; set; }
        public virtual IList<MembershipRuleComprehensionTestResult> MembershipRuleComprehensionTestResults { get; set; }
        public virtual IList<QuranComment> QuranComments { get; set; }
        //public virtual HumanPublicProfile PublicProfile { get; set; }
        //public virtual PublicProfilePicture Picture { get; set; }
    }
    //public class HumanPublicProfile
    //{
    //    public int HumanId { get; set; }
    //    public virtual Human Human { get; set; }
    //    public string Introduction { get; set; }
    //    public string WebsiteUrl { get; set; }
    //}
    //public class PublicProfilePicture
    //{
    //    public int HumanId { get; set; }
    //    public virtual Human Human { get; set; }
    //    public byte[] ImgData { get; set; }
    //}

    //public class MotherRelationship
    //{
    //    public int HumanId { get; set; }
    //    public virtual Human Human { get; set; }
    //    public int MotherHumanId { get; set; }
    //    public virtual Human Mother{ get; set; }
    //    public DateTime RegistrationDateTimeUtc { get; set; }
    //}
    //public class FatherRelationship
    //{
    //    public int HumanId { get; set; }
    //    public virtual Human Human { get; set; }
    //    public int FatherHumanId { get; set; }
    //    public virtual Human Father { get; set; }
    //    public DateTime RegistrationDateTimeUtc { get; set; }
    //}

    //public class Birth
    //{
    //    public int HumanId { get; set; }
    //    public virtual Human Human { get; set; }
    //    public int DateOfBirthYear { get; set; }
    //    public int DateOfBirthMonth { get; set; }
    //    public int DateOfBirthDay { get; set; }
    //    public string CityOrRegionOfBirth { get; set; }
    //    public string CountryOfBirth { get; set; }
    //    public DateTime RegistrationDateTimeUtc { get; set; }
    //}

    //public class Death
    //{
    //    public int HumanId { get; set; }
    //    public virtual Human Human { get; set; }
    //    public int DateOfDeathYear { get; set; }
    //    public int DateOfDeathMonth { get; set; }
    //    public int DateOfDeathDay { get; set; }
    //    public string CityOrRegionOfDeath { get; set; }
    //    public string CountryOfDeath { get; set; }
    //    public DateTime RegistrationDateTimeUtc { get; set; }
    //}
}
