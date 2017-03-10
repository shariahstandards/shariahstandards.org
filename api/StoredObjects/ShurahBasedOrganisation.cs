using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects
{
    public class ShurahBasedOrganisation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IList<MembershipApplication> MembershipApplications { get; set; }
        public virtual IList<Member> Members { get; set; } 
        public DateTime LastUpdateDateTimeUtc { get; set; }
        public virtual IList<Action> Actions { get; set; } 
        public bool Closed { get; set; }
        public string UrlDomain { get; set; }
        public virtual IList<MembershipRuleSection> MembershipRuleSections { get; set; }
        public JoiningPolicy JoiningPolicy { get; set; }
        public virtual OrganisationLeader OrganisationLeader { get; set; }
        public virtual IList<MembershipRuleTermDefinition> Terms { get; set; }
        public virtual OrganisationRelationship ParentOrganisationRelationship { get; set; }
        public virtual IList<OrganisationRelationship> ChildOrganisationRelationships { get; set; }
        public int RequiredNumbersOfAcceptingMembers { get; set; }
    }
}
