using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using StoredObjects;
using WebApiResources;

namespace Services
{
    public interface IUserServiceDependencies
    {
        IStorageService StorageService { get; set; }
        ILinqService LinqService { get; set; }
    }
    public class UserServiceDependencies : IUserServiceDependencies
    {
        public IStorageService StorageService { get; set; }
        public ILinqService LinqService { get; set; }

        public UserServiceDependencies(IStorageService storageService,ILinqService linqService)
        {
            StorageService = storageService;
            LinqService = linqService;
        }
    }
    public interface IUserService
    {
        UserProfileResource GetUserProfile(Auth0UserProfile auth0UserProfile, IPrincipal principal);
        Auth0User GetAuthenticatedUser(IPrincipal principal);
        Auth0User GetGuaranteedAuthenticatedUser(IPrincipal principal);
    }
    public class UserService:IUserService
    {
        private readonly IUserServiceDependencies _dependencies;
        public UserService(IUserServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual UserProfileResource GetUserProfile(Auth0UserProfile auth0UserProfile, IPrincipal principal)
        {
            var claimsIdentity = principal.Identity as ClaimsIdentity;

            VerifyProfile(auth0UserProfile, claimsIdentity);
            var user =
                _dependencies.StorageService.SetOf<Auth0User>().FirstOrDefault(x => x.Id == auth0UserProfile.user_id);
            if (user == null)
            {
                user = BuildAuth0User(auth0UserProfile);
                _dependencies.StorageService.SetOf<Auth0User>().Add(user);
                _dependencies.StorageService.SaveChanges();
            }
            else
            {
                user.PictureUrl = auth0UserProfile.Picture;
                _dependencies.StorageService.SaveChanges();
            }
            return BuildUserProfileResource(user);
        }

        public virtual Auth0User GetAuthenticatedUser(IPrincipal principal)
        {
            var userId = GetLoggedInUserId(principal.Identity as ClaimsIdentity);
            return _dependencies.LinqService.SingleOrDefault(
                _dependencies.StorageService.SetOf<Auth0User>(), u => u.Id == userId);
        }

        public virtual Auth0User GetGuaranteedAuthenticatedUser(IPrincipal principal)
        {
            var user = GetAuthenticatedUser(principal);
            if (user == null)
            {
                throw new Exception("Access Denied");
            }
            return user;
        }

        public virtual Auth0User BuildAuth0User(Auth0UserProfile auth0Profile)
        {
            var user = _dependencies.StorageService.SetOf<Auth0User>().Create();
            user.Id = auth0Profile.user_id;
            user.PictureUrl = auth0Profile.Picture;
            user.Name = auth0Profile.Name;
            return user;
        }

        public virtual void VerifyProfile(Auth0UserProfile auth0Profile, ClaimsIdentity claimsIdentity)
        {
            var authenticatedUserId = GetLoggedInUserId(claimsIdentity);
            if (authenticatedUserId != null && authenticatedUserId == auth0Profile.user_id)
            {
                return;
            }
            throw new AccessViolationException("Not authenticated for the identified user");
        }

        public virtual string GetLoggedInUserId(ClaimsIdentity claimsIdentity)
        {
            var authenticatedUserId = claimsIdentity.Claims.ToList()
                .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claimsIdentity.IsAuthenticated && authenticatedUserId != null)
            {
                return authenticatedUserId.Value;
            }
            return null;
        }

        public virtual UserProfileResource BuildUserProfileResource(Auth0User user)
        {
            return new UserProfileResource
            {
                Name=user.Name,
                PictureUrl = user.PictureUrl,
                UserId = user.Id
            };
        }
    }
}
