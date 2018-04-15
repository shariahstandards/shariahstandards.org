using System;
using System.Collections.Generic;

namespace StoredObjects
{
    public class MembershipApplication
    {
        public virtual ShurahBasedOrganisation Organisation { get; set; }
        public int OrganisationId { get; set; }
        public int Id { get; set; }
        public virtual Auth0User Auth0User { get; set; }
        public string Auth0UserId { get; set; }
        public string SupportingStatement { get; set; }
        public string Email { get; set; }
        public virtual IList<MembershipApplicationAcceptance> Acceptances { get; set; }
        public string PhoneNumber { get; set; }
        public string PublicName { get; set; }
        public DateTime DateAppliedUtc { get; set; }
    }
}