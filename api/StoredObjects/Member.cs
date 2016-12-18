using System;
using System.Collections.Generic;

namespace StoredObjects
{
    public class Member
    {
        public int Id { get; set; }
        public DateTime JoinedOnDateAndTimeUtc{ get; set; }
        public virtual ShurahBasedOrganisation Organisation { get; set; }
        public int OrganisationId { get; set; }
        public virtual IList<MemberAuth0User> MemberAuth0Users { get; set; } 
        public virtual LeaderRecognition LeaderRecognition { get; set; }
        public virtual IList<LeaderRecognition> Followers { get; set; } 
        public bool Moderated { get; set; }
        public bool Removed { get; set; }
        public DateTime LastDateAndTimeUtcAgreedToMembershipRules { get; set; }
        public string Introduction { get; set; }
        public virtual IList<MembershipInvitation> Invitations { get; set; }
        public virtual IList<MembershipApplicationAcceptance> MemberAcceptances { get; set; }
        public virtual IList<Suggestion> Suggestions { get; set; }
        public virtual IList<SuggestionVote> SuggestionVotes { get; set; }
        public virtual IList<ActionUpdate> ActionUpdates { get; set; }
        public virtual IList<MembershipRuleViolationAccusation> ReceivedMembershipRuleViolationAccusations { get; set; }
        public virtual IList<MembershipRuleViolationAccusation> MadeMembershipRuleViolationAccusations { get; set; }
        public string PublicName { get; set; }

        //public virtual IList<MembershipRuleAgreementViolationClaim> MembershipRuleAgreementViolationClaims { get; set; }
    }
}