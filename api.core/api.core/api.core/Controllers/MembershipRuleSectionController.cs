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
    public class MembershipRuleSectionController : Controller
    {
        private readonly IMembershipRuleSectionService _service;

        public MembershipRuleSectionController(IMembershipRuleSectionService service)
        {
            _service = service;
        }

        [Route("api/CreateRuleSection")]
        public ResponseResource Post(CreateMembershipRuleSectionRequest request)
        {
            return _service.CreateRuleSection(User,request);
        }
        [Route("api/UpdateRuleSection")]
        public ResponseResource Post(UpdateMembershipRuleSectionRequest request)
        {
            return _service.UpdateRuleSection(User, request);
        }
        [Route("api/DragDropRuleSection")]
        public ResponseResource Post(DragDropMembershipRuleSectionRequest request)
        {
            return _service.DragDropRuleSection(User, request);
        }
        [Route("api/DeleteRuleSection")]
        public ResponseResource Post(DeleteMembershipRuleSectionRequest request)
        {
            return _service.DeleteRuleSection(User, request);
        }
    }
}
