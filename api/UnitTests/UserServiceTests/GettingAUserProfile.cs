using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using StoredObjects;
using WebApiResources;

namespace UnitTests
{
    public class GettingAUserProfile : UserServiceTestContext
    {
        public class UserIsUnknown : UserServiceTestContext
        {
            [Test]
            public void NewUserProfileIsAddedAndResourceReturned()
            {
                MethodToTest(() => service.GetUserProfile(A<Auth0UserProfile>.Ignored, A<IPrincipal>.Ignored));

                var auth0Profile = new Auth0UserProfile();
                var auth0UserSet = new FakeDbSet<Auth0User>();
                A.CallTo(() => dependencies.StorageService.SetOf<Auth0User>()).Returns(auth0UserSet);
                var user = new Auth0User();
                A.CallTo(() => service.BuildAuth0User(auth0Profile)).Returns(user);
                var claimsIdentity = new ClaimsIdentity();
                var resource = new UserProfileResource();
                A.CallTo(() => service.BuildUserProfileResource(user)).Returns(resource);
                var principal = A.Fake<IPrincipal>();
                A.CallTo(() => principal.Identity).Returns(claimsIdentity);

                var result = service.GetUserProfile(auth0Profile, principal);

                Assert.AreSame(resource, result);
                A.CallTo(() => service.VerifyProfile(auth0Profile, claimsIdentity)).MustHaveHappened();
                Assert.AreEqual(1, auth0UserSet.Count());
                Assert.AreSame(user, auth0UserSet.First());
                A.CallTo(() => dependencies.StorageService.SaveChanges()).MustHaveHappened();
            }

            public class BuildingAnAuth0User : UserServiceTestContext
            {
                [Test]
                public void PropertiesSetCorrectly()
                {
                    MethodToTest(() => service.BuildAuth0User(A<Auth0UserProfile>.Ignored));

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

                    Assert.AreEqual(auth0Profile.user_id, result.Id);
                    Assert.AreEqual(auth0Profile.Picture, result.PictureUrl);
                    Assert.AreEqual(auth0Profile.Name, result.Name);
                    Assert.AreSame(user, result);

                }
            }
        }

        public class UserIsKnown : UserServiceTestContext
        {
            [Test]
            public void UserIsVerifiedAndResourceReturned()
            {
                MethodToTest(() => service.GetUserProfile(A<Auth0UserProfile>.Ignored, A<IPrincipal>.Ignored));

                var user = new Auth0User {Id = "someId"};
                var auth0Profile = new Auth0UserProfile {user_id = user.Id};
                var auth0UserSet = new FakeDbSet<Auth0User> {user};
                A.CallTo(() => dependencies.StorageService.SetOf<Auth0User>()).Returns(auth0UserSet);
                A.CallTo(() => service.BuildAuth0User(auth0Profile)).Returns(user);
                var claimsIdentity = new ClaimsIdentity();
                var resource = new UserProfileResource();
                A.CallTo(() => service.BuildUserProfileResource(user)).Returns(resource);
                var principal = A.Fake<IPrincipal>();
                A.CallTo(() => principal.Identity).Returns(claimsIdentity);



                var result = service.GetUserProfile(auth0Profile, principal);

                Assert.AreSame(resource, result);
                A.CallTo(() => service.VerifyProfile(auth0Profile, claimsIdentity)).MustHaveHappened();
            }
        }

        public class VerifyingAUserProfile : UserServiceTestContext
        {
            public class UserIsRecognised : UserServiceTestContext
            {
                [Test]
                public void NoExceptionIsThrown()
                {
                    MethodToTest(()=>service.VerifyProfile(A<Auth0UserProfile>.Ignored,A<ClaimsIdentity>.Ignored));

                    var profile = new Auth0UserProfile {user_id = "someUserId"};
                    var claimsIdentity = A.Fake<ClaimsIdentity>();
                    A.CallTo(() => service.GetLoggedInUserId(claimsIdentity)).Returns(profile.user_id);
                    //A.CallTo(()=> claimsIdentity.Claims).Returns(
                    //    new[] { new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",profile.user_id) });
                    A.CallTo(() => claimsIdentity.IsAuthenticated).Returns(true);

                    service.VerifyProfile(profile, claimsIdentity);
                }
            }
            public class UserIsNotAuthenticated : UserServiceTestContext
            {
                [Test]
                public void NoExceptionIsThrown()
                {
                    MethodToTest(() => service.VerifyProfile(A<Auth0UserProfile>.Ignored, A<ClaimsIdentity>.Ignored));

                    var profile = new Auth0UserProfile {user_id = "someUserId"};
                    var claimsIdentity = A.Fake<ClaimsIdentity>();
                    A.CallTo(() => service.GetLoggedInUserId(claimsIdentity)).Returns(null);
                    //A.CallTo(() => claimsIdentity.Claims).Returns(
                    //    new[]
                    //    {
                    //        new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                    //            profile.user_id)
                    //    });
                    //A.CallTo(() => claimsIdentity.IsAuthenticated).Returns(false);

                    var exception =
                        Assert.Throws<AccessViolationException>(() => service.VerifyProfile(profile, claimsIdentity));

                    Assert.AreEqual("Not authenticated for the identified user",exception.Message);
                }
            }
        }

        public class GettingALoggedInUserId : UserServiceTestContext
        {
            public class ClaimNotFound : UserServiceTestContext
            {
                [Test]
                public void NullReturned()
                {
                    MethodToTest(() => service.GetLoggedInUserId(A<ClaimsIdentity>.Ignored));

                    var claimsIdentity = A.Fake<ClaimsIdentity>();
                    A.CallTo(() => claimsIdentity.Claims).Returns(
                        new Claim[] {});

                    var result = service.GetLoggedInUserId(claimsIdentity);

                    Assert.IsNull(result);

                }
            }

            public class ClaimFound : UserServiceTestContext
            {
                public class UserNotAuthenticated : UserServiceTestContext
                {
                    [Test]
                    public void NullReturned()
                    {
                        MethodToTest(() => service.GetLoggedInUserId(A<ClaimsIdentity>.Ignored));

                        var claimsIdentity = A.Fake<ClaimsIdentity>();
                        var userId = "some user id";
                        A.CallTo(() => claimsIdentity.Claims).Returns(
                            new Claim[]
                            {
                                  new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",userId
                               )
                            });
                        A.CallTo(() => claimsIdentity.IsAuthenticated).Returns(false);

                        var result = service.GetLoggedInUserId(claimsIdentity);

                        Assert.IsNull(result);

                    }
                }
                public class UserIsAuthenticated : UserServiceTestContext
                {
                    [Test]
                    public void NullReturned()
                    {
                        MethodToTest(() => service.GetLoggedInUserId(A<ClaimsIdentity>.Ignored));

                        var claimsIdentity = A.Fake<ClaimsIdentity>();
                        var userId = "some user id";
                        A.CallTo(() => claimsIdentity.Claims).Returns(
                            new Claim[]
                            {
                                  new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",userId
                               )
                            });
                        A.CallTo(() => claimsIdentity.IsAuthenticated).Returns(true);

                        var result = service.GetLoggedInUserId(claimsIdentity);

                        Assert.AreEqual(userId,result);

                    }
                }
            }
        }

        public class BuildUserProfileResource : UserServiceTestContext
        {
            [Test]
            public void PropertiesAreSetCorrectly()
            {
                MethodToTest(()=>service.BuildUserProfileResource(A<Auth0User>.Ignored));

                var user = new Auth0User
                {
                    Name = "a"
                };

                var result = service.BuildUserProfileResource(user);

                Assert.AreEqual(user.Name,result.Name);
                Assert.AreEqual(user.PictureUrl,result.PictureUrl);
                Assert.AreEqual(user.Id,result.UserId);
            }
        }
    }
}
