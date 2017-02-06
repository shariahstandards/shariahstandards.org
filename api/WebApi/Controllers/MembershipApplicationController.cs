using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;

namespace WebApi.Controllers
{
    public class MembershipApplicationController : ApiController
    {
        private readonly IMembershipApplicationService _service;

        public MembershipApplicationController(IMembershipApplicationService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("Join")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.SubmitApplication(User));
        }
    }
}
