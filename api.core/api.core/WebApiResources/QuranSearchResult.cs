using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
  public class QuranSearchResult
  {
    public string SearchText { get; set; }
    public List<SearchResultCategory> ResultCategories { get; set; }

  }
  public class SearchResultCategory
  {
    public string Match { get; set; }
    public List<VerseResult> Results { get; set; }
    public string MatchType { get; set; }
  }
  public class VerseResult
  {
    public int SurahNumber { get; set; }
    public int VerseNumber { get; set; }
    public int? WordNumber { get; set; }
    public int? WordPartNumber { get; set; }
  }
}
