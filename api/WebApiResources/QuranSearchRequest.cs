using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
  public class QuranSearchRequest
  {
    public string SearchText { get; set; }
    public bool SearchInEnglish { get; set; }
  }
}
