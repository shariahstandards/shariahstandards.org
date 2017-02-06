using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebApiResources;

namespace Services
{
    public interface IMembershipCancellationServiceDependencies
    {
        IUserService UserService { get; set; }
        IStorageService StorageService { get; set; }
    }
    public class MembershipCancellationServiceDependencies : IMembershipCancellationServiceDependencies
    {
        public IUserService UserService { get; set; }
        public IStorageService StorageService { get; set; }

        public MembershipCancellationServiceDependencies(IUserService userService,
            IStorageService storageService)
        {
            UserService = userService;
            StorageService = storageService;
        }
    }
    public interface IMembershipCancellationService
    {
        MembershipCancellationResponseResource SubmitCancellation(IPrincipal principal);
    }
    public class MembershipCancellationService : IMembershipCancellationService
    {
        private readonly IMembershipCancellationServiceDependencies _dependencies;

        public MembershipCancellationService(IMembershipCancellationServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual MembershipCancellationResponseResource SubmitCancellation(IPrincipal principal)
        {
            var user = _dependencies.UserService.GetAuthenticatedUser(principal);
            if (user == null)
            {
                return new MembershipCancellationResponseResource
                {
                    HasError = true,
                    Error = "unable to find user"
                };
            }
            var membership =
                user.MemberAuth0Users.FirstOrDefault(x => x.Member.Organisation.ParentOrganisationRelationship == null);
            if (membership != null)
            {
                membership.Member.Removed = true;
                _dependencies.StorageService.SaveChanges();
            }
            return new MembershipCancellationResponseResource
            {
                HasError = false,
            };
        }
    }
}
