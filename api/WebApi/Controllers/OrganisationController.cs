using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;

namespace WebApi.Controllers
{
    [RoutePrefix("api")]
    public class OrganisationController : ApiController
    {
        private readonly IOrganisationService _service;

        public OrganisationController(IOrganisationService service)
        {
            _service = service;
        }
        [Route("RootOrganisation")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetRootOrganisation(User));
        }
    }
}
