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
        [HttpPost]
        [Route("GetOrganisationSummary")]
        public HttpResponseMessage GetOrganisationSummary(GetOrganisationSummaryRequest request) 
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetOrganisationSummary(User,request));
        }

        [Route("GetPermissionsForOrganisation/{organisationId}")]
        public HttpResponseMessage Get(int organisationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetPermission(User, organisationId));
        }
        [Route("ViewAllPermissionsForOrganisation/{organisationId}")]
        public HttpResponseMessage GetAllPermissions(int organisationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetAllPermissions(User, organisationId));
        }
        [HttpPost]
        [Route("DelegatePermission")]
        public HttpResponseMessage PostDelegatePermission(AddDelegatedPermissionRequest request) 
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.DelegatePermission(User, request));
        }
        [HttpPost]
        [Route("DelegatePermission")]
        public HttpResponseMessage PostRemoveDelegatedPermission(RemoveDelegatedPermissionRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.RemoveDelegatedPermission(User, request));
        }
        [Route("GetTermDefinition/{termId}/{organisationId}")]
        public HttpResponseMessage GetTermDefinition(int termId,int organisationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetTermDefinition(termId,organisationId));
        }
    }
}
