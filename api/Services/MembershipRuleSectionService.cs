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
        ResponseResource DragDropRuleSection(IPrincipal user, DragDropMembershipRuleSectionRequest request);
        ResponseResource DeleteRuleSection(IPrincipal user, DeleteMembershipRuleSectionRequest request);
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

        public ResponseResource DragDropRuleSection(IPrincipal principal, DragDropMembershipRuleSectionRequest request)
        {
            var user = _dependencies.UserService.GetAuthenticatedUser(principal);
            var draggedSection = GetMembershipRuleSection(request.DraggedMembershipRuleSectionId);
            var permissions = _dependencies.OrganisationService.GetMemberPermissions(user, draggedSection);
            if (!permissions.Contains(ShurahOrganisationPermission.EditMembershipRules.ToString()))
            {
                return new ResponseResource { HasError = true, Error = "Access Denied!" };
            }
            var droppedOnSection = GetMembershipRuleSection(request.DroppedOnMembershipRuleSectionId);
            var dropPermissions = _dependencies.OrganisationService.GetMemberPermissions(user, droppedOnSection);
            if (!dropPermissions.Contains(ShurahOrganisationPermission.EditMembershipRules.ToString()))
            {
                return new ResponseResource { HasError = true, Error = "Access Denied!" };
            }
            var siblingSections = droppedOnSection.ShurahBasedOrganisation.MembershipRuleSections.Where(s =>
                (s.ParentMembershipRuleSection == null && droppedOnSection.ParentMembershipRuleSection == null)
                || (s.ParentMembershipRuleSection!=null && droppedOnSection.ParentMembershipRuleSection!=null &&
                s.ParentMembershipRuleSection.ParentMembershipRuleSectionId ==
                    droppedOnSection.ParentMembershipRuleSection.ParentMembershipRuleSectionId)
                ).OrderBy(x=>x.Sequence).ToList();
            Enumerable.Range(0,siblingSections.Count()).ToList().ForEach(i =>
            {
                siblingSections[i].Sequence = i*2;
            });

            if (draggedSection.ParentMembershipRuleSection == null &&
                droppedOnSection.ParentMembershipRuleSection != null)
            {
                draggedSection.ParentMembershipRuleSection = new MembershipRuleSectionRelationship();
            }
            if (draggedSection.ParentMembershipRuleSection!=null && droppedOnSection.ParentMembershipRuleSection != null)
            {
                draggedSection.ParentMembershipRuleSection.ParentMembershipRuleSectionId
                    = droppedOnSection.ParentMembershipRuleSection.ParentMembershipRuleSectionId;
            }
            if (draggedSection.ParentMembershipRuleSection != null && droppedOnSection.ParentMembershipRuleSection == null)
            {
                _dependencies.StorageService.SetOf<MembershipRuleSectionRelationship>().Remove(draggedSection.ParentMembershipRuleSection);
            }
            draggedSection.Sequence = droppedOnSection.Sequence + 1;
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

        public virtual ResponseResource DeleteRuleSection(IPrincipal principal, DeleteMembershipRuleSectionRequest request)
        {
            var user = _dependencies.UserService.GetAuthenticatedUser(principal);
            var sectionToDelete= GetMembershipRuleSection(request.MembershipRuleSectionId);
            var permissions = _dependencies.OrganisationService.GetMemberPermissions(user, sectionToDelete);
            if (!permissions.Contains(ShurahOrganisationPermission.EditMembershipRules.ToString()))
            {
                return new ResponseResource { HasError = true, Error = "Access Denied!" };
            }
            if (sectionToDelete.ParentMembershipRuleSection != null)
            {
                _dependencies.StorageService.SetOf<MembershipRuleSectionRelationship>().Remove(sectionToDelete.ParentMembershipRuleSection);
            }
            _dependencies.StorageService.SetOf<MembershipRuleSection>().Remove(sectionToDelete);
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

        public virtual MembershipRuleSection GetMembershipRuleSection(int membershipRuleSectionId)
        {
            return _dependencies.StorageService.SetOf<MembershipRuleSection>().SingleOrDefault(s=>s.Id==membershipRuleSectionId);
        }
    }
}
