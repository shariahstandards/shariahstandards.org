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
    public class MemberController : ApiController
    {
        private readonly IMemberService _service;

        public MemberController(IMemberService service)
        {
            _service = service;
        }

        [Route("SearchForMembers")]
        public HttpResponseMessage SearchForMembers(SearchMemberRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.SearchForMembers(User, request));
        }
    }
}
