using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class MembershipCancellationResponseResource
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
