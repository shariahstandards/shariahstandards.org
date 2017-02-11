using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using StoredObjects;
using WebApiResources;

namespace Services
{
    public interface IMembershipRuleServiceDependencies
    {
        IUserService UserService { get; set; }
        IStorageService StorageService { get; set; }
        IMembershipRuleSectionService MembershipRuleSectionService { get; set; }
        IOrganisationService OrganisationService { get; set; }
    }
    public class MembershipRuleServiceDependencies : IMembershipRuleServiceDependencies
    {
        public IUserService UserService { get; set; }
        public IStorageService StorageService { get; set; }
        public IMembershipRuleSectionService MembershipRuleSectionService { get; set; }
        public IOrganisationService OrganisationService { get; set; }

        public MembershipRuleServiceDependencies(IUserService userService,
            IStorageService storageService,
            IMembershipRuleSectionService membershipRuleSectionService,
            IOrganisationService organisationService
            )
        {
            UserService = userService;
            StorageService = storageService;
            MembershipRuleSectionService = membershipRuleSectionService;
            OrganisationService = organisationService;
        }
    }
    public interface IMembershipRuleService
    {
        ResponseResource CreateRule(IPrincipal user, CreateMembershipRuleRequest request);
        ResponseResource UpdateRule(IPrincipal user, UpdateMembershipRuleRequest request);
        ResponseResource DragAndDropRule(IPrincipal user, DragAndDropMembershipRuleRequest request);
        ResponseResource DeleteRule(IPrincipal user, DeleteMembershipRuleRequest request);
    }
    public class MembershipRuleService: IMembershipRuleService
    {
        private readonly IMembershipRuleServiceDependencies _dependencies;

        public MembershipRuleService(IMembershipRuleServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public ResponseResource CreateRule(IPrincipal principal, CreateMembershipRuleRequest request)
        {
            var user = _dependencies.UserService.GetAuthenticatedUser(principal);
            var ruleSection = _dependencies.MembershipRuleSectionService.GetMembershipRuleSection(request.MembershipRuleSectionId);
            var permissions = _dependencies.OrganisationService.GetMemberPermissions(user, ruleSection);

            if (!permissions.Contains(ShurahOrganisationPermission.EditMembershipRules.ToString()))
            {
                return new ResponseResource { HasError = true, Error = "Access Denied!" };
            }
            var rule = _dependencies.StorageService.SetOf<MembershipRule>().Create();
            _dependencies.StorageService.SetOf<MembershipRule>().Add(rule);
            rule.MembershipRuleSection = ruleSection;
            rule.RuleStatement = request.Rule;
            rule.MembershipRuleSectionId = ruleSection.Id;
            rule.Sequence = ruleSection.MembershipRules.Count + 2;
            rule.PublishedDateTimeUtc=DateTime.UtcNow;
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

        public ResponseResource UpdateRule(IPrincipal principal, UpdateMembershipRuleRequest request)
        {
            var user = _dependencies.UserService.GetAuthenticatedUser(principal);
            var rule = GetMembershipRule(request.MembershipRuleId);
            var permissions = _dependencies.OrganisationService.GetMemberPermissions(user, rule);

            if (!permissions.Contains(ShurahOrganisationPermission.EditMembershipRules.ToString()))
            {
                return new ResponseResource { HasError = true, Error = "Access Denied!" };
            }
            rule.RuleStatement = request.Rule;
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

        public ResponseResource DragAndDropRule(IPrincipal principal, DragAndDropMembershipRuleRequest request)
        {
            var user = _dependencies.UserService.GetAuthenticatedUser(principal);
            var draggedRule = GetMembershipRule(request.DraggedMembershipRuleId);
            var permissionsForDraggedRule = _dependencies.OrganisationService.GetMemberPermissions(user, draggedRule);

            if (!permissionsForDraggedRule.Contains(ShurahOrganisationPermission.EditMembershipRules.ToString()))
            {
                return new ResponseResource { HasError = true, Error = "Access Denied!" };
            }
            var droppedRule = GetMembershipRule(request.DroppedMembershipRuleId);
            var permissionsForDroppedRule = _dependencies.OrganisationService.GetMemberPermissions(user, droppedRule);

            if (!permissionsForDroppedRule.Contains(ShurahOrganisationPermission.EditMembershipRules.ToString()))
            {
                return new ResponseResource { HasError = true, Error = "Access Denied!" };
            }
            var siblingRules = droppedRule.MembershipRuleSection.MembershipRules.OrderBy(x => x.Sequence).ToList();
            Enumerable.Range(0, siblingRules.Count()).ToList().ForEach(i =>
            {
                siblingRules[i].Sequence = i * 2;
            });
            draggedRule.Sequence = droppedRule.Sequence + 1;
            draggedRule.MembershipRuleSection = droppedRule.MembershipRuleSection;
            draggedRule.MembershipRuleSectionId = draggedRule.MembershipRuleSectionId;
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

        public virtual ResponseResource DeleteRule(IPrincipal principal, DeleteMembershipRuleRequest request)
        {
            var user = _dependencies.UserService.GetAuthenticatedUser(principal);
            var rule = GetMembershipRule(request.MembershipRuleId);
            var permissionsForRule = _dependencies.OrganisationService.GetMemberPermissions(user, rule);
            if (!permissionsForRule.Contains(ShurahOrganisationPermission.EditMembershipRules.ToString()))
            {
                return new ResponseResource { HasError = true, Error = "Access Denied!" };
            }
            _dependencies.StorageService.SetOf<MembershipRule>().Remove(rule);
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }


        public virtual MembershipRule GetMembershipRule(int membershipRuleId)
        {
            return _dependencies.StorageService.SetOf<MembershipRule>().SingleOrDefault(r => r.Id == membershipRuleId);
        }
    }
}
