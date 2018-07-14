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
    public class MembershipCancellationController : Controller
    {
        private readonly IMembershipCancellationService _service;

        public MembershipCancellationController(IMembershipCancellationService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("api/Leave")]
        public MembershipCancellationResponseResource Post(LeaveOrganisationRequest request)
        {
            return _service.SubmitCancellation(request,User);
        }
    }
}
