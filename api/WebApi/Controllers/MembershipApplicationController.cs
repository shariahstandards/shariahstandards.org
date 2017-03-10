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
    public class MembershipApplicationController : ApiController
    {
        private readonly IMembershipApplicationService _service;

        public MembershipApplicationController(IMembershipApplicationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("ApplyToJoin")]
        public HttpResponseMessage Post(MembershipApplicationrequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.ApplyToJoin(User,request));
        }

        [HttpPost]
        [Route("ViewApplications")]
        public HttpResponseMessage Post(MembershipApplicationSearchRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.SearchMembershipApplications(User, request));
        }

        [HttpPost]
        [Route("AcceptMembershipApplication")]
        public HttpResponseMessage Post(MembershipApplicationAcceptanceRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.AcceptMembershipApplication(User, request));
        }
        [HttpPost]
        [Route("RejectMembershipApplication")]
        public HttpResponseMessage Post(MembershipApplicationRejectionRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.RejectMembershipApplication(User, request));
        }
    }
}
