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
    public class MembershipRuleSectionController : ApiController
    {
        private readonly IMembershipRuleSectionService _service;

        public MembershipRuleSectionController(IMembershipRuleSectionService service)
        {
            _service = service;
        }

        [Route("CreateRuleSection")]
        public HttpResponseMessage Post(CreateMembershipRuleSectionRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.CreateRuleSection(User,request));
        }
        [Route("UpdateRuleSection")]
        public HttpResponseMessage Post(UpdateMembershipRuleSectionRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.UpdateRuleSection(User, request));
        }
        [Route("DragDropRuleSection")]
        public HttpResponseMessage Post(DragDropMembershipRuleSectionRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.DragDropRuleSection(User, request));
        }
        [Route("DeleteRuleSection")]
        public HttpResponseMessage Post(DeleteMembershipRuleSectionRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.DeleteRuleSection(User, request));
        }
    }
}
