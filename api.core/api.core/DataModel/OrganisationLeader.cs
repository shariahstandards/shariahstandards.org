using System;

namespace DataModel
{
    public class OrganisationLeader
    {
        public virtual ShurahBasedOrganisation Organisation { get; set; }
        public int OrganisationId { get; set; }
        public int LeaderMemberId { get; set; }
        public virtual Member Leader { get; set; }
        public DateTime LastUpdateDateTimeUtc { get; set; }
    }
}