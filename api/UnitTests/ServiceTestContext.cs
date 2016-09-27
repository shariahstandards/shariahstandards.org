using System;
using System.Linq.Expressions;
using FakeItEasy;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ServiceTestContext<S, D>
    {
        protected D dependencies;
        protected S service;
        protected void MethodToTest(Expression<Action> testedMethod)
        {
            dependencies = A.Fake<D>();

            var dependenciesType = typeof(D);
            var serviceType = typeof(S);
            var constructorInfo = serviceType.GetConstructor(new[] { dependenciesType });

            var serviceNew = Expression.New(constructorInfo, Expression.Constant(dependencies));
            var lambda = (Expression<Func<S>>)Expression.Lambda(serviceNew);

            service = A.Fake<S>(x => x.WithArgumentsForConstructor(lambda));
            A.CallTo(testedMethod).CallsBaseMethod();
        }
    }
}