using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class CreateTermDefinitionRequest
    {
        public int OrganisationId { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
    }
    public class UpdateTermDefinitionRequest
    {
        public int TermId { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
    }
    public class DeleteTermDefinitionRequest
    {
        public int TermId { get; set; }
    }
    public class UpdateTermDefinitionResponse:ResponseResource
    {
        public int TermId { get; set; }
        public string Term { get; set; }
    }
    public class CreateTermDefinitionResponse:ResponseResource
    {
        public int Id { get; set; }
        public string Term { get; set; }
    }
}
