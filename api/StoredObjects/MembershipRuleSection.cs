using System;
using System.Collections.Generic;

namespace StoredObjects
{
    public class MembershipRuleSection
    {
        public virtual ShurahBasedOrganisation ShurahBasedOrganisation { get; set; }
        public int ShurahBasedOrganisationId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int Sequence { get; set; }
        public virtual MembershipRuleSectionRelationship ParentMembershipRuleSection { get; set; }
        public virtual IList<MembershipRuleSectionRelationship> ChildMembershipRuleSections { get; set; }
        public DateTime PublishedDateTimeUtc { get; set; }
        public virtual IList<MembershipRule> MembershipRules { get; set; }
    }
}