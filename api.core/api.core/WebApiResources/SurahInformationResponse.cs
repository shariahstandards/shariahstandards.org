using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
  public class SurahInformationResponse
  {
    public int Number { get; set; }
    public string ArabicName { get; set; }
    public string EnglishName { get; set; }
    public int VerseCount { get; set; }
  }
}
