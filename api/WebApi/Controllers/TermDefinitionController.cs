using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;
using WebApiResources;

namespace WebApi.Controllers
{
    public class TermDefinitionController : ApiController
    {
        private readonly ITermDefinitionService _service;

        public TermDefinitionController(ITermDefinitionService service)
        {
            _service = service;
        }

        [Route("CreateTermDefinition")]
        public HttpResponseMessage Post(CreateTermDefinitionRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.CreateTermDefinition(User, request));
        }
    }
}
