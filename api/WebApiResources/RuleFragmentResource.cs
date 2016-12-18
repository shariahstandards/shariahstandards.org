using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class RuleFragmentResource
    {
        public string Text { get; set; }
        public bool IsPlainText { get; set; }
        public int TermId { get; set; }
    }
}
