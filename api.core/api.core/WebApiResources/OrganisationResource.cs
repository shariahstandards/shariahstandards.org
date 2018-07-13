using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class OrganisationSummaryResource
    {
        public int Id { get; set; }
        public MemberResource Member { get; set; }
        public List<string> Permissions { get; set; }
    }
    public class OrganisationResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string JoiningPolicy { get; set; }
        public MemberResource Member { get; set; }
        public List<MembershipRuleSectionResource> RuleSections { get; set; }
        public List<string> Permissions { get; set; }
        public MemberResource LeaderMember { get; set; }
        public bool HasPendingApplication { get; set; }
        public int PendingMembershipApplicationsCount { get; set; }
    }
}
