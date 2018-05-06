using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
  public class VerseResponse
  {
    public List<ArabicWordResource> ArabicWords { get; set; }
    public string EnglishText { get; set; }
  }
  public class ArabicWordResource
  {
    public string Text { get; set; }
    public List<string> Prefixes { get; set; }
    public List<string> Suffixes { get; set; }
    public string Stem { get; set; }
    public string Root { get; set; }
  }
}
