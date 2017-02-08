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
    public interface IMembershipRuleSectionServiceDependencies
    {
        IUserService UserService { get; set; }
        IOrganisationService OrganisationService { get; set; }
        IStorageService StorageService { get; set; }
    }
    public class MembershipRuleSectionServiceDependencies : IMembershipRuleSectionServiceDependencies
    {
        public IUserService UserService { get; set; }
        public IOrganisationService OrganisationService { get; set; }
        public IStorageService StorageService { get; set; }

        public MembershipRuleSectionServiceDependencies(IUserService userService,
            IOrganisationService organisationService,
            IStorageService storageService)
        {
            UserService = userService;
            OrganisationService = organisationService;
            StorageService = storageService;
        }
    }
    public interface IMembershipRuleSectionService
    {
        ResponseResource CreateRuleSection(IPrincipal principal, CreateMembershipRuleSectionRequest request);
    }
    public class MembershipRuleSectionService: IMembershipRuleSectionService
    {
        private readonly IMembershipRuleSectionServiceDependencies _dependencies;

        public MembershipRuleSectionService(IMembershipRuleSectionServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual ResponseResource CreateRuleSection(IPrincipal principal, CreateMembershipRuleSectionRequest request)
        {
            var user = _dependencies.UserService.GetAuthenticatedUser(principal);
            var organisation = _dependencies.OrganisationService.GetOrganisation(request.OrganisationId);
            var permissions = _dependencies.OrganisationService.GetMemberPermissions(user, organisation);

            if (!permissions.Contains(ShurahOrganisationPermission.EditMembershipRules.ToString()))
            {
                return new ResponseResource {HasError=true,Error="Access Denied!"};
            }
            if (organisation.MembershipRuleSections.Any(s => s.UniqueInOrganisationName == request.UniqueUrlSlug))
            {
                return new ResponseResource {HasError=true,Error="Url id not unique!"};
            }
            var ruleSection = _dependencies.StorageService.SetOf<MembershipRuleSection>().Create();
            _dependencies.StorageService.SetOf<MembershipRuleSection>().Add(ruleSection);
            if (request.ParentSectionId.HasValue)
            {
                var parentSection =
                    organisation.MembershipRuleSections.FirstOrDefault(x => x.Id == request.ParentSectionId);
                if (parentSection != null)
                {
                    ruleSection.ParentMembershipRuleSection = new MembershipRuleSectionRelationship
                    {
                        ParentMembershipRuleSectionId = parentSection.Id,

                    };
                }
            }
            ruleSection.ShurahBasedOrganisationId = organisation.Id;
            ruleSection.UniqueInOrganisationName = request.UniqueUrlSlug;
            ruleSection.PublishedDateTimeUtc = DateTime.UtcNow;
            ruleSection.Title = request.Title;
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }
    }
}
