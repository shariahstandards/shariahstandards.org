using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class TermDefinitionResource:ResponseResource
    {
        public string Term { get; set; }
        public int Id { get; set; }
        public string RawDefinition { get; set; }
        public List<TextFragmentResource> Definition { get; set; }
    }
}
