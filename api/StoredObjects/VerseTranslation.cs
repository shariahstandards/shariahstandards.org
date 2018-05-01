using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects
{
  public class VerseTranslation
  {
    public int SurahNumber { get; set; }
    public int VerseNumber { get; set; }
    public string Text { get; set; }
    public virtual Verse Verse { get; set; }
  }
}
