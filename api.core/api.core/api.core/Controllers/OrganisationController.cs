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
   // [RoutePrefix("api")]
    public class OrganisationController : Controller
    {
        private readonly IOrganisationService _service;

        public OrganisationController(IOrganisationService service)
        {
            _service = service;
        }
        [Route("api/GetOrganisation/{organisationId}")]
        public OrganisationResource Get(int organisationId)
        {
            return _service.GetOrganisation(User,organisationId);
        }
        [HttpPost]
        [Route("api/GetOrganisationSummary")]
        public OrganisationSummaryResource GetOrganisationSummary([FromBody]GetOrganisationSummaryRequest request) 
        {
            return _service.GetOrganisationSummary(User,request);
        }

        [Route("api/GetPermissionsForOrganisation/{organisationId}")]
        public List<string> GetPermissions(int organisationId)
        {
            return _service.GetPermission(User, organisationId);
        }
        [Route("api/ViewAllPermissionsForOrganisation/{organisationId}")]
        public AllOrganisationPermissionsResource GetAllPermissions(int organisationId)
        {
            return _service.GetAllPermissions(User, organisationId);
        }
        [HttpPost]
        [Route("api/DelegatePermission")]
        public ResponseResource PostDelegatePermission([FromBody]AddDelegatedPermissionRequest request) 
        {
            return _service.DelegatePermission(User, request);
        }
        [HttpPost]
        [Route("api/DelegatePermission")]
        public ResponseResource PostRemoveDelegatedPermission([FromBody]RemoveDelegatedPermissionRequest request)
        {
            return _service.RemoveDelegatedPermission(User, request);
        }
        [Route("api/GetTermDefinition/{termId}/{organisationId}")]
        public TermDefinitionResource GetTermDefinition(int termId,int organisationId)
        {
            return _service.GetTermDefinition(termId,organisationId);
        }
        [Route("api/GetTermList/{organisationId}")]
        public TermListResource GetTermList(int organisationId)
        {
            return _service.GetTermList(User,organisationId);
        }
        
    }
}
