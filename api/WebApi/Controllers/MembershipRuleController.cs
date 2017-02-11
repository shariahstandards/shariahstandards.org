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
    public class MembershipRuleController : ApiController
    {
        private readonly IMembershipRuleService _service;

        public MembershipRuleController(IMembershipRuleService service)
        {
            _service = service;
        }

        [Route("CreateRule")]
        public HttpResponseMessage Post(CreateMembershipRuleRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.CreateRule(User, request));
        }
        [Route("UpdateRule")]
        public HttpResponseMessage Post(UpdateMembershipRuleRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.UpdateRule(User, request));
        }
        [Route("DeleteRule")]
        public HttpResponseMessage Post(DeleteMembershipRuleRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.DeleteRule(User, request));
        }
        [Route("DragAndDropRule")]
        public HttpResponseMessage Post(DragAndDropMembershipRuleRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.DragAndDropRule(User, request));
        }
    }
}
