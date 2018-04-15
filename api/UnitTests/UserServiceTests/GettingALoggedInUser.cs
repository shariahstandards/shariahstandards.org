using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using StoredObjects;
using WebApiResources;

namespace UnitTests.UserServiceTests
{
    public class GettingALoggedInUser:UserServiceTestContext
    {
        [Test]
        public void ReturnsTheSingleMatchingUser()
        {
            MethodToTest(()=>service.GetAuthenticatedUser(A<IPrincipal>.Ignored));

            var claimsIdentity = new ClaimsIdentity();
            var principal = A.Fake<IPrincipal>();
            A.CallTo(() => principal.Identity).Returns(claimsIdentity);
            var userId = "someUserId";
            A.CallTo(() => service.GetLoggedInUserId(claimsIdentity)).Returns(userId);
            var users = A.Fake<IDbSet<Auth0User>>();
            A.CallTo(() => dependencies.StorageService.SetOf<Auth0User>()).Returns(users);
            var user = new Auth0User();
            A.CallTo(() => dependencies.LinqService.SingleOrDefault(users,
                A<Expression<Func<Auth0User, bool>>>.That.Matches(x =>
                    x.Compile().Invoke(new Auth0User {Id = userId})
                    && !x.Compile().Invoke(new Auth0User {Id = userId + "x"})
                    ))).Returns(user);

            var result = service.GetAuthenticatedUser(principal);

            Assert.AreSame(user,result);
        }
    }
}
