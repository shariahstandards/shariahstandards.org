using System.Collections.Generic;

namespace WebApiResources
{
    public class SearchMemberRequest
    {
        public int OrganisationId { get; set; }
        public int? Page { get; set; }
    }
    public class SearchMemberResponse : ResponseResource
    {
        public int OrganisationId { get; set; }
        public string OrganisationName { get; set; }
        public int PageCount { get; set; }
        public List<SearchedMemberResource> Members { get; set; }
    }

    public class SearchedMemberResource
    {
        public int Id { get; set; }
        public string PublicName { get; set; }
        public string PictureUrl { get; set; }
        public int Followers { get; set; }
        public string Introduction { get; set; }
        public string LastCalculated { get; set; }
        public bool IsLeader { get; set; }
  }
    
}
