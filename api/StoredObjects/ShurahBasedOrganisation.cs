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
        public string ConditionsOfMembership { get; set; }
        public virtual IList<Member> Members { get; set; } 
        public virtual Auth0User Founder { get; set; }
        public DateTime LastUpdateDateTimeUtc { get; set; }
    }

    public class OrganisationLeader
    {
        public virtual ShurahBasedOrganisation Organisation { get; set; }
        public int OrganisationId { get; set; }
        public int LeaderMemberId { get; set; }
        public virtual Member Leader { get; set; }
        public DateTime LastUpdateDateTimeUtc { get; set; }
    }

    public class Suggestion
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string FullText { get; set; }
        public int AuthorMemberId { get; set; }
        public virtual Member AuthorMember { get; set; }
    }

    public enum CommentType
    {
        Neutral=0,
        Support=1,
        Oppose=2
    }
    public class SuggestionComment
    {
        public int Id { get; set; }
        public int SuggestionId { get; set; }
        public virtual Suggestion Suggestion { get; set; }
        public CommentType CommentType { get; set; }
        public string Comment { get; set; }
        public int CommenterMemberId { get; set; }
        public virtual Member CommenterMember { get; set; }
        public DateTime LastUpdateDateTimeUtc { get; set; }
    }

    public class LeaderRecognition
    {
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public int RecognisedLeaderMemberId { get; set; }
        public virtual Member RecognisedLeaderMember { get; set; }
        public DateTime LastUpdateDateTimeUtc { get; set; }
    }

    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Suggestion> AuthoredSuggestions { get; set; } 
        public DateTime JoinedOnDateAndTimeUtc{ get; set; }
        public virtual ShurahBasedOrganisation Organisation { get; set; }
        public int OrganisationId { get; set; }
        public virtual IList<LoginUser> LoginUsers { get; set; } 
        public virtual LeaderRecognition LeaderRecognition { get; set; }
        public virtual IList<LeaderRecognition> Followers { get; set; } 
        public bool Moderated { get; set; }
        public string Introduction { get; set; }
    }
    public class LoginUser
    {
        public int Id { get; set; }
        public virtual Auth0User Login { get; set; }
        public string LoginUserId { get; set; }
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
    }

    public class MembershipApplication
    {
        public virtual ShurahBasedOrganisation Organisaion { get; set; }
        public int OrganisationId { get; set; }
        public int Id { get; set; }
        public virtual Auth0User Login { get; set; }
        public string LoginUserId { get; set; }
        public string SupportingStatement { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
    }
}
