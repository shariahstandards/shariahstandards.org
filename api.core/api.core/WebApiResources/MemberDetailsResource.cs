using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class MemberDetailsResource:ResponseResource
    {
        public int MemberId { get; set; }
        public string PublicName { get; set; }
        public string PictureUrl { get; set; }
        public int DelegatedVotesCount { get; set; }
        public PrivateMemberDetailsResource PrivateMemberDetails { get; set; }
        public bool IsFollowedByCurrentUser { get; set; }
    }

    public class PrivateMemberDetailsResource
    {
        public SearchedMemberResource Leader { get; set; }
        public bool SendNoEmailNotifications { get; set; }
    }
}
