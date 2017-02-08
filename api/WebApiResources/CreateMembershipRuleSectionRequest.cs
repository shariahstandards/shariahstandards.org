using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class CreateMembershipRuleSectionRequest
    {
        public int OrganisationId { get; set; }
        public int? ParentSectionId { get; set; }
        public string Title { get; set; }
        public string UniqueUrlSlug { get; set; }
    }
}
