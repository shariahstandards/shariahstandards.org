using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class MembershipApplicationResponseResource
    {
        public bool NowAMember { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
