using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;

namespace WebApi.Controllers
{
    public class MembershipRuleController : ApiController
    {
        private readonly IMembershipRuleService _service;

        public MembershipRuleController(IMembershipRuleService service)
        {
            _service = service;
        }

        [Route("CreateRule")]
        public HttpResponseMessage Get(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
