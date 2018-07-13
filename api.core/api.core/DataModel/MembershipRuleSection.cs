using System;
using System.Collections.Generic;

namespace DataModel
{
    public class MembershipRuleSection
    {
        public virtual ShurahBasedOrganisation ShurahBasedOrganisation { get; set; }
        public int ShurahBasedOrganisationId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string UniqueInOrganisationName { get; set; }
        public int Sequence { get; set; }
        public virtual MembershipRuleSectionRelationship ParentMembershipRuleSection { get; set; }
        public virtual IList<MembershipRuleSectionRelationship> ChildMembershipRuleSections { get; set; }
        public DateTime PublishedDateTimeUtc { get; set; }
        public virtual IList<MembershipRule> MembershipRules { get; set; }
        public virtual IList<MembershipRuleSectionAcceptance> MembershipRuleSectionAcceptances { get; set; }
        //public bool MustBeAcceptedToBeAMember { get; set; }
    }

    public class MembershipRuleSectionAcceptance
    {
        public int Id { get; set; }
        public virtual Auth0User Auth0User { get; set; }
        public string Auth0UserId { get; set; }
        public virtual MembershipRuleSection MembershipRuleSection { get; set; }
        public virtual int MembershipRuleSectionId { get; set; }
        public DateTime AcceptedDateTimeUtc { get; set; }
    }
}