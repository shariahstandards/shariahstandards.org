using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class TermListResource
    {
        public List<TermResource> Terms { get; set; }
        public string OrganisationName { get; set; }
        public int OrganisationId { get; set; }
    }

    public class TermResource
    {
        public string Term { get; set; }
        public int TermId { get; set; }
    }
}
