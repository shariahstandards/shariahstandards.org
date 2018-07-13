using System;

namespace DataModel
{
    public class MembershipApplicationAcceptance
    {
        public int Id { get; set; }
        public int MembershipApplicationId { get; set; }
        public virtual MembershipApplication MembershipApplication { get; set; }
        public int AcceptingMemberId { get; set; }
        public virtual Member AcceptingMember { get; set; }
        public DateTime AcceptanceDateTimeUtc { get; set; }
    }
}