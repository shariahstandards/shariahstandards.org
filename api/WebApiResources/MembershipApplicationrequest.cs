using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class MembershipApplicationrequest
    {
        public int OrganisationId { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PublicName { get; set; }
        public string PublicProfileStatement { get; set; }
        public bool AgreesToTermsAndConditions { get; set; }
    }


    public class MembershipApplicationSearchRequest
    {
        public int OrganisationId { get; set; }
        public int Page { get; set; }

    }
    public class MembershipApplicationSearchResultsResource:ResponseResource
    {
        public int OrganisationId { get; set; }
        public List<MembershipApplicationResource> Results { get; set; } 
    }

    public class MembershipApplicationResource
    {
        public int Id { get; set; }
        public string PublicName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string SupportingStatement { get; set; }
        public string PictureUrl { get; set; }
    }

    public class MembershipApplicationAcceptanceRequest
    {
        public int Id { get; set; }
    }
    public class MembershipApplicationRejectionRequest
    {
        public int Id { get; set; }
        public string Reason { get; set; }
    }
}
