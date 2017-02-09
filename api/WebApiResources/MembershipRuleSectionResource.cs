using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class MembershipRuleSectionResource
    {
        public List<MembershipRuleResource> Rules { get; set; }
        public List<MembershipRuleSectionResource> SubSections { get; set; }
        public string Title { get; set; }
        public string UniqueName { get; set; }
        public int Id { get; set; }
    }
    public class MembershipRuleResource
    {
        public string Number { get; set; }
        public int Id { get; set; }
        public List<RuleFragmentResource> RuleFragments { get; set; }
        public string ExplanationUrl { get; set; }
        public int ComprehensionScore { get; set; }
        public int MaxComprehensionScore { get; set; }
        public string PublishedUtcDateTimeText { get; set; }
    }

    public class MemberResource
    {
        public int Id { get; set; }
        public int DirectFollowers { get; set; }
        public int IndirectFollowers { get; set; }
        public int ToDoCount { get; set; }
        public string LeaderPublicName { get; set; }
        public string PublicName { get; set; }
    }
}
