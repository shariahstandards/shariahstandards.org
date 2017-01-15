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
        public bool IsTerm { get; set; }
        public QuranReferenceResource QuranReference { get; set; }
        public int TermId { get; set; }
    }

    public class QuranReferenceResource
    {
        public int Surah { get; set; }
        public int Verse { get; set; }
     //  public int? StartWord { get; set; }
     //  public int? EndWord { get; set; }
    }
}
