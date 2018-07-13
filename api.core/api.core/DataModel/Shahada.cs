using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
  public class Declaration
  {
    public string Auth0UserId { get; set; }
    public virtual Auth0User Auth0User { get; set; }

  }
}
