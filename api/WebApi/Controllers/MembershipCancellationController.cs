using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;

namespace WebApi.Controllers
{
    public class MembershipCancellationController : ApiController
    {
        private readonly IMembershipCancellationService _service;

        public MembershipCancellationController(IMembershipCancellationService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("Leave")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.SubmitCancellation(User));
        }
    }
}
