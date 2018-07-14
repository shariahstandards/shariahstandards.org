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
    public class MembershipRuleController : Controller
    {
        private readonly IMembershipRuleService _service;

        public MembershipRuleController(IMembershipRuleService service)
        {
            _service = service;
        }

        [Route("api/CreateRule")]
        public ResponseResource Post(CreateMembershipRuleRequest request)
        {
            return _service.CreateRule(User, request);
        }
        [Route("api/UpdateRule")]
        public ResponseResource Post(UpdateMembershipRuleRequest request)
        {
            return _service.UpdateRule(User, request);
        }
        [Route("api/DeleteRule")]
        public ResponseResource Post(DeleteMembershipRuleRequest request)
        {
            return _service.DeleteRule(User, request);
        }
        [Route("api/DragAndDropRule")]
        public ResponseResource Post(DragAndDropMembershipRuleRequest request)
        {
            return _service.DragAndDropRule(User, request);
        }
    }
}
