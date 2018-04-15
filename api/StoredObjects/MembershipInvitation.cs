using System;

namespace StoredObjects
{
    public class MembershipInvitation
    {
        public int Id { get; set; }
        public string EmailAddressList { get; set; }
        public DateTime DateTimeInvitationsSetUtc { get; set; }
        public int InviterMemberId { get; set; }
        public virtual Member InviterMember { get; set; }
    }
}