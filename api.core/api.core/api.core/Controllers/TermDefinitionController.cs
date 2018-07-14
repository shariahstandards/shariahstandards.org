using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApiResources;

namespace WebApi.Controllers
{
    public class TermDefinitionController : Controller
    {
        private readonly ITermDefinitionService _service;

        public TermDefinitionController(ITermDefinitionService service)
        {
            _service = service;
        }

        [Route("api/CreateTermDefinition")]
        public CreateTermDefinitionResponse Post(CreateTermDefinitionRequest request)
        {
            return _service.CreateTermDefinition(User, request);
        }
        [Route("api/UpdateTermDefinition")]
        public UpdateTermDefinitionResponse Post(UpdateTermDefinitionRequest request)
        {
            return _service.UpdateTermDefinition(User, request);
        }
        [Route("api/DeleteTermDefinition")]
        public ResponseResource Post(DeleteTermDefinitionRequest request)
        {
            return _service.DeleteTermDefinition(User, request);
        }
    }
}
