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
        [HttpPost]
        public HttpResponseMessage SearchForMembers(SearchMemberRequest request)
        {
          return Request.CreateResponse(HttpStatusCode.OK, _service.SearchForMembers(User, request));
        }
        [Route("MemberDetails/{memberId}")]
        [HttpGet]
        public HttpResponseMessage MembersDetails(int memberId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.GetMemberDetails(User, memberId));
        }
        [Route("FollowMember/{memberId}")]
        [HttpGet]
        public HttpResponseMessage FollowMember(int memberId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.FollowMember(User, memberId));
        }
        [Route("StopFollowingAMember/{organisationId}")]
        [HttpGet]
        public HttpResponseMessage StopFollowingAMember(int organisationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.StopFollowingAMember(User, organisationId));
        }
    }
}
