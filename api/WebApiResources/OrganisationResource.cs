using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class OrganisationResource
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string JoiningPolicy { get; set; }
        public MemberResource Member { get; set; }
        public List<MembershipRuleSectionResource> RuleSections { get; set; }
    }
}
