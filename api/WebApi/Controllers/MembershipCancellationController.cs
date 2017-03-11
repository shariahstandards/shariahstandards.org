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
    public class MembershipCancellationController : ApiController
    {
        private readonly IMembershipCancellationService _service;

        public MembershipCancellationController(IMembershipCancellationService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("Leave")]
        public HttpResponseMessage Post(LeaveOrganisationRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, value: _service.SubmitCancellation(request,User));
        }
    }
}
