using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;

namespace WebApi.Controllers
{
   // [RoutePrefix("api")]
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

        [Route("GetPermissionsForOrganisation/{organisationId}")]
        public HttpResponseMessage Get(int organisationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetPermission(User, organisationId));
        }
        [Route("GetTermDefinition/{termId}/{organisationId}")]
        public HttpResponseMessage GetTermDefinition(int termId,int organisationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetTermDefinition(termId,organisationId));
        }
    }
}
