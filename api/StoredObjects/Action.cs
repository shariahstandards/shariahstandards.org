using System;
using System.Collections.Generic;

namespace StoredObjects
{
    public class Action
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime TimeAndDateOfDecisionUtc { get; set; }
        public virtual ShurahBasedOrganisation Organisation { get; set; }
        public int OrganisationId { get; set; }
        public virtual IList<ActionUpdate> Updates { get; set; }
        public int Priority { get; set; }
    }
}