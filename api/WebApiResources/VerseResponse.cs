using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
  public class VerseResponse
  {
    public List<string> ArabicWords { get; set; }
    public string EnglishText { get; set; }
  }
}
