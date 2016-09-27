using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using StoredObjects;
using WebApiResources;

namespace UnitTests
{
    public class TrackingAUserLogin:UserServiceTestContext
    {
        public class UserIsUnknown:UserServiceTestContext
        {
            [Test]
            public void NewUserProfileIsAddedAndResourceReturned()
            {
                MethodToTest(()=>service.TrackLogin(A<Auth0UserProfile>.Ignored,A<ClaimsIdentity>.Ignored));

                var auth0Profile=new Auth0UserProfile();
                var auth0UserSet=new FakeDbSet<Auth0User>();
                A.CallTo(() => dependencies.StorageService.SetOf<Auth0User>()).Returns(auth0UserSet);
                var user = new Auth0User();
                A.CallTo(() => service.BuildAuth0User(auth0Profile)).Returns(user);
                var claimsIdentity = new ClaimsIdentity();
                var resource = new UserProfileResource();
                A.CallTo(() => service.BuildUserProfileResource(user)).Returns(resource);

                var result = service.TrackLogin(auth0Profile,claimsIdentity);

                Assert.AreSame(resource, result);
                A.CallTo(() => service.VerifyProfile(auth0Profile, claimsIdentity)).MustHaveHappened();
                Assert.AreEqual(1,auth0UserSet.Count());
                Assert.AreSame(user,auth0UserSet.First());
                A.CallTo(() => dependencies.StorageService.SaveChanges()).MustHaveHappened();
            }

            public class BuildingAnAuth0User : UserServiceTestContext
            {
                [Test]
                public void PropertiesSetCorrectly()
                {
                    MethodToTest(()=>service.BuildAuth0User(A<Auth0UserProfile>.Ignored));

                    var auth0Profile = new Auth0UserProfile
                    {
                        Name = "someone",
                        user_id = "something from facebookand auth0",
                        Picture = "some url - maybe just a fake pic"
                    };
                    var fakeSet = A.Fake<IDbSet<Auth0User>>();
                    A.CallTo(() => dependencies.StorageService.SetOf<Auth0User>()).Returns(fakeSet);
                    var user = new Auth0User();
                    A.CallTo(() => fakeSet.Create()).Returns(user);

                    var result = service.BuildAuth0User(auth0Profile);

                    Assert.AreEqual(auth0Profile.user_id,result.Id);
                    Assert.AreEqual(auth0Profile.Picture,result.PictureUrl);
                    Assert.AreEqual(auth0Profile.Name,result.Name);
                    Assert.AreSame(user,result);
                
                }
            }
        }

        public class UserIsKnown : UserServiceTestContext
        {
            [Test]
            public void UserIsVerifiedAndResourceReturned()
            {
                MethodToTest(() => service.TrackLogin(A<Auth0UserProfile>.Ignored, A<ClaimsIdentity>.Ignored));

                var user = new Auth0User {Id = "someId"};
                var auth0Profile = new Auth0UserProfile { user_id = user.Id };
                var auth0UserSet = new FakeDbSet<Auth0User> { user};
                A.CallTo(() => dependencies.StorageService.SetOf<Auth0User>()).Returns(auth0UserSet);
                A.CallTo(() => service.BuildAuth0User(auth0Profile)).Returns(user);
                var claimsIdentity = new ClaimsIdentity();
                var resource = new UserProfileResource();
                A.CallTo(() => service.BuildUserProfileResource(user)).Returns(resource);

                var result = service.TrackLogin(auth0Profile, claimsIdentity);

                Assert.AreSame(resource,result);
                A.CallTo(() => service.VerifyProfile(auth0Profile, claimsIdentity)).MustHaveHappened();
            }
        }
    }
    }
