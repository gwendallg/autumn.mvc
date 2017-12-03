using System;
using System.Linq.Expressions;
using Autumn.Mvc.Configurations;
using Autumn.Mvc.Models.Queries;
using Xunit;

namespace Autumn.Mvc.Tests.Models.Queries
{
    public class QueryModelBinderProviderTest
    {
        [Fact]
        public void IsNotQueryModelBinder()
        {
            var modelBinderProviderContextMock = new ModelBinderProviderContextMock(typeof(string));
            var expected = new QueryModelBinderProvider(new AutumnSettings());
            Assert.Null(expected.GetBinder(modelBinderProviderContextMock));
        }

        [Fact]
        public void IsQueryModelBinder()
        {
            var modelBinderProviderContextMock =
                new ModelBinderProviderContextMock(typeof(Expression<Func<string, bool>>));
            var expected = new QueryModelBinderProvider(new AutumnSettings());
            Assert.NotNull(expected.GetBinder(modelBinderProviderContextMock));
        }
    }

}