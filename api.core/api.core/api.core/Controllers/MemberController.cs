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
    public class MemberController : Controller
    {
        private readonly IMemberService _service;

        public MemberController(IMemberService service)
        {
            _service = service;
        }

        [Route("api/SearchForMembers")]
        [HttpPost]
        public SearchMemberResponse SearchForMembers(SearchMemberRequest request)
        {
          return _service.SearchForMembers(User, request);
        }
        [Route("api/MemberDetails/{memberId}")]
        [HttpGet]
        public MemberDetailsResource MembersDetails(int memberId)
        {
            return _service.GetMemberDetails(User, memberId);
        }
        [Route("api/FollowMember/{memberId}")]
        [HttpGet]
        public ResponseResource FollowMember(int memberId)
        {
            return _service.FollowMember(User, memberId);
        }
        [Route("api/StopFollowingAMember/{organisationId}")]
        [HttpGet]
        public ResponseResource StopFollowingAMember(int organisationId)
        {
            return _service.StopFollowingAMember(User, organisationId);
        }
    }
}
