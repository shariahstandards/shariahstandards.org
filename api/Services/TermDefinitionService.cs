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
    public interface ITermDefinitionServiceDependencies
    {
        IUserService UserService { get; set; }
        IOrganisationService OrganisationService { get; set; }
        IStorageService StorageService { get; set; }
    }
    public class TermDefinitionServiceDependencies : ITermDefinitionServiceDependencies
    {
        public IUserService UserService { get; set; }
        public IOrganisationService OrganisationService { get; set; }
        public IStorageService StorageService { get; set; }

        public TermDefinitionServiceDependencies(IUserService userService,
            IOrganisationService organisationService,
            IStorageService storageService)
        {
            UserService = userService;
            OrganisationService = organisationService;
            StorageService = storageService;
        }
    }
    public interface ITermDefinitionService
    {
        CreateTermDefinitionResponse CreateTermDefinition(IPrincipal principal, CreateTermDefinitionRequest request);
    }
    public class TermDefinitionService: ITermDefinitionService
    {
        private readonly ITermDefinitionServiceDependencies _dependencies;

        public TermDefinitionService(ITermDefinitionServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual CreateTermDefinitionResponse CreateTermDefinition(IPrincipal principal, CreateTermDefinitionRequest request)
        {
            var user = _dependencies.UserService.GetGuaranteedAuthenticatedUser(principal);
            var organisation = _dependencies.OrganisationService.GetOrganisation(request.OrganisationId);
            _dependencies.OrganisationService.GuaranteeUserHasPermission(user,organisation,ShurahOrganisationPermission.EditMembershipRules);

            if (organisation.Terms.Any(t => t.Term == request.Term))
            {
                return new CreateTermDefinitionResponse
                {
                    HasError = true,
                    Error = "This term already exists"
                };
            }
            var defn=_dependencies.StorageService.SetOf<MembershipRuleTermDefinition>().Create();
            defn.Term = request.Term;
            defn.Definition = request.Definition;
            defn.Organisation = organisation;
            defn.OrganisationId = request.OrganisationId;
            _dependencies.StorageService.SetOf<MembershipRuleTermDefinition>().Add(defn);
            _dependencies.StorageService.SaveChanges();
            return new CreateTermDefinitionResponse
            {
                Id = defn.Id,
                Term = defn.Term
            };
        }
    }
}
