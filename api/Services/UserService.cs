using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using StoredObjects;
using WebApiResources;

namespace Services
{
    public interface IUserServiceDependencies
    {
        IStorageService StorageService { get; set; }
    }
    public class UserServiceDependencies : IUserServiceDependencies
    {
        public IStorageService StorageService { get; set; }

        public UserServiceDependencies(IStorageService storageService)
        {
            StorageService = storageService;
        }
    }
    public interface IUserService
    {
        UserProfileResource TrackLogin(Auth0UserProfile auth0UserProfile, ClaimsIdentity claimsIdentity);
    }
    public class UserService:IUserService
    {
        private readonly IUserServiceDependencies _dependencies;
        public UserService(IUserServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual UserProfileResource TrackLogin(Auth0UserProfile auth0UserProfile, ClaimsIdentity claimsIdentity)
        {
            VerifyProfile(auth0UserProfile, claimsIdentity);
            var user =
                _dependencies.StorageService.SetOf<Auth0User>().FirstOrDefault(x => x.Id == auth0UserProfile.user_id);
            if (user == null)
            {
                user = BuildAuth0User(auth0UserProfile);
                _dependencies.StorageService.SetOf<Auth0User>().Add(user);
                _dependencies.StorageService.SaveChanges();
            }
            return BuildUserProfileResource(user);
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
            var authenticatedUserId = claimsIdentity.Claims.ToList()
                .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (authenticatedUserId != null && claimsIdentity.IsAuthenticated &&
                authenticatedUserId.Value == auth0Profile.user_id)
            {
                return;
            }
            throw new AccessViolationException("Not authenticated for the identified user");
        }

        public virtual UserProfileResource BuildUserProfileResource(Auth0User user)
        {
            return new UserProfileResource();
            //throw new NotImplementedException();
        }
    }
}
