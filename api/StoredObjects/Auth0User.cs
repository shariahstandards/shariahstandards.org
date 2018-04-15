using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects
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
    }

}
