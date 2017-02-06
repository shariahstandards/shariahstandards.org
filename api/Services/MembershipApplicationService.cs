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
    public interface IMembershipApplicationServiceDependencies
    {
        IStorageService StorageService { get; set; }
        IUserService UserService { get; set; }

    }
    public class MembershipApplicationServiceDependencies : IMembershipApplicationServiceDependencies
    {
        public IStorageService StorageService { get; set; }
        public IUserService UserService { get; set; }

        public MembershipApplicationServiceDependencies(IStorageService storageService,
            IUserService userService)
        {
            StorageService = storageService;
            UserService = userService;
        }
    }
    public interface IMembershipApplicationService
    {
        MembershipApplicationResponseResource SubmitApplication(IPrincipal principal);

    }
    public class MembershipApplicationService: IMembershipApplicationService
    {
        private readonly IMembershipApplicationServiceDependencies _dependencies;

        public MembershipApplicationService(IMembershipApplicationServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual MembershipApplicationResponseResource SubmitApplication(IPrincipal principal)
        {
            var user = _dependencies.UserService.GetAuthenticatedUser(principal);
            if (user == null)
            {
                return new MembershipApplicationResponseResource
                {
                    HasError = true,
                    Error = "unable to find user"
                };
            }
            var membership =
                user.MemberAuth0Users.FirstOrDefault(x => x.Member.Organisation.ParentOrganisationRelationship == null);
            if (membership!=null)
            {
                membership.Member.Removed = false;
                membership.Member.LastDateAndTimeUtcAgreedToMembershipRules=DateTime.UtcNow;
                _dependencies.StorageService.SaveChanges();
                return new MembershipApplicationResponseResource
                {
                    HasError = false,
                };
            }
            var rootOrganisation =
                _dependencies.StorageService.SetOf<ShurahBasedOrganisation>()
                    .FirstOrDefault(x => x.ParentOrganisationRelationship == null);

            var member = _dependencies.StorageService.SetOf<Member>().Create();
            member.JoinedOnDateAndTimeUtc = DateTime.Now;
            member.PublicName = user.Name;
            member.Introduction = "My name is " + user.Name;
            member.Organisation = rootOrganisation;
            member.LastDateAndTimeUtcAgreedToMembershipRules=DateTime.UtcNow;
            _dependencies.StorageService.SetOf<Member>().Add(member);
            _dependencies.StorageService.SaveChanges();

            var memberAuthUser = _dependencies.StorageService.SetOf<MemberAuth0User>().Create();
            memberAuthUser.Member = member;
            memberAuthUser.Auth0User = user;
            _dependencies.StorageService.SetOf<MemberAuth0User>().Add(memberAuthUser);

            _dependencies.StorageService.SaveChanges();

            return new MembershipApplicationResponseResource
            {
                NowAMember = true
            };

        }
    }
}
