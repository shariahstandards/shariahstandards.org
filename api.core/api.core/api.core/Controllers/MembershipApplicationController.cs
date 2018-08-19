using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApiResources;

namespace WebApi.Controllers
{
  [Authorize]

  public class MembershipApplicationController : Controller
    {
        private readonly IMembershipApplicationService _service;

        public MembershipApplicationController(IMembershipApplicationService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("api/ApplyToJoin")]
        public ResponseResource Post([FromBody]MembershipApplicationrequest request)
        {
            return _service.ApplyToJoin(User,request);
        }

        [HttpPost]
        [Route("api/ViewApplications")]
        public MembershipApplicationSearchResultsResource Post([FromBody]MembershipApplicationSearchRequest request)
        {
            return _service.SearchMembershipApplications(User, request);
        }

        [HttpPost]
        [Route("api/AcceptMembershipApplication")]
        public ResponseResource Post([FromBody]MembershipApplicationAcceptanceRequest request)
        {
            return _service.AcceptMembershipApplication(User, request);
        }
        [HttpPost]
        [Route("api/RejectMembershipApplication")]
        public ResponseResource Post([FromBody]MembershipApplicationRejectionRequest request)
        {
            return _service.RejectMembershipApplication(User, request);
        }
    }
}
