using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
  public class Auth0User
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public virtual IList<MemberAuth0User> MemberAuth0Users { get; set; }
    public virtual IList<MembershipApplication> MembershipApplications { get; set; }
    public virtual IList<ContactDetail> ContactDetails { get; set; }
    public virtual IList<MembershipRuleSectionAcceptance> MembershipRuleSectionAcceptances { get; set; }
    public virtual IList<MembershipRuleComprehensionTestResult> MembershipRuleComprehensionTestResults { get; set; }
    public virtual IList<QuranComment> QuranComments { get; set; }
    public virtual UserFirstName FirstName { get; set; }
    public virtual UserFathersFirstName FathersName { get; set; }
    public virtual UserBirthLocation BirthLocation { get; internal set; }
    public virtual UserEmail Email { get; internal set; }
    public virtual UserMobilePhone MobilePhone { get; internal set; }
    public virtual UserPhotograph Photograph { get; internal set; }
  }

}
