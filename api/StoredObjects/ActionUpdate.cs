using System;

namespace StoredObjects
{
    public class ActionUpdate
    {
        public int Id { get; set; }
        public int ActionId { get; set; }
        public virtual Action Action { get; set; }
        public DateTime UpdateDateTimeUtc { get; set; }
        public string UpdatedDescription { get; set; }
        public ActionStatus Status { get; set; }
        public virtual Member ResponsibleMember { get; set; }
        public int ResponsibleMemberId { get; set; }
        public decimal? HoursWorkedSincePreviousUpdate { get; set; }


    }
}