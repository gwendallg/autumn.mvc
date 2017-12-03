using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Autumn.Mvc.Configurations;
using Autumn.Mvc.Models.Paginations;
using Autumn.Mvc.Models.Paginations.Exceptions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace Autumn.Mvc.Tests.Models.Paginations
{
    public class PageableModelBinderTest
    {
        [Fact]
        public void OutOfRangePageSizeException()
        {
            var queryColletion = new QueryCollection(
                new Dictionary<string, StringValues>(new[]
                    {
                        new KeyValuePair<string, StringValues>("PageSize", new StringValues("-1"))
                    }
                ));
            var pageableModelBinder = new PageableModelBinder<object>(new AutumnSettings());
            Assert.Throws<OutOfRangePageSizeException>(() => pageableModelBinder.Build(queryColletion));
        }
        
        [Fact]
        public void InvalidPageSizeValueException()
        {
            var queryColletion = new QueryCollection(
                new Dictionary<string, StringValues>(new[]
                    {
                        new KeyValuePair<string, StringValues>("PageSize", new StringValues("a"))
                    }
                ));
            var pageableModelBinder = new PageableModelBinder<object>(new AutumnSettings());
            Assert.Throws<InvalidPageSizeValueException>(() => pageableModelBinder.Build(queryColletion));
        }
        
        [Fact]
        public void OutOfRangePageNumberException()
        {
            var queryColletion = new QueryCollection(
                new Dictionary<string, StringValues>(new[]
                    {
                        new KeyValuePair<string, StringValues>("PageNumber", new StringValues("-1"))
                    }
                ));
            var pageableModelBinder = new PageableModelBinder<object>(new AutumnSettings());
            Assert.Throws<OutOfRangePageNumberException>(() => pageableModelBinder.Build(queryColletion));
        }

        [Fact]
        public void InvalidPageNumberValueException()
        {
            var queryColletion = new QueryCollection(
                new Dictionary<string, StringValues>(new[]
                    {
                        new KeyValuePair<string, StringValues>("PageNumber", new StringValues("a"))
                    }
                ));
            var pageableModelBinder = new PageableModelBinder<object>(new AutumnSettings());
            Assert.Throws<InvalidPageNumberValueException>(() => pageableModelBinder.Build(queryColletion));
        }
        
        [Fact]
        public void UnknownSortException()
        {
           var queryColletion = new QueryCollection(
                new Dictionary<string, StringValues>(new[]
                    {
                        new KeyValuePair<string, StringValues>("Sort", new StringValues("a"))
                    }
                ));
            var pageableModelBinder = new PageableModelBinder<Customer>(new AutumnSettings());
            Assert.Throws<UnknownSortException>(() => pageableModelBinder.Build(queryColletion));
        }

        [Fact]
        public void InvalidSortDirectionException()
        {
            var queryColletion = new QueryCollection(
                new Dictionary<string, StringValues>(new[]
                    {
                        new KeyValuePair<string, StringValues>("Sort", new StringValues("Name")),
                        new KeyValuePair<string, StringValues>("Name.Dir", new StringValues("t")),
                    }
                ));
            var pageableModelBinder = new PageableModelBinder<Customer>(new AutumnSettings());
            Assert.Throws<InvalidSortDirectionException>(() => pageableModelBinder.Build(queryColletion));
        }

        [Fact]
        public void IsValid()
        {
            var queryColletion = new QueryCollection(
                new Dictionary<string, StringValues>(new[]
                    {
                        new KeyValuePair<string, StringValues>("PageSize", new StringValues("1")),
                        new KeyValuePair<string, StringValues>("PageNumber", new StringValues("2")),
                        new KeyValuePair<string, StringValues>("Name.Dir", new StringValues(new[] {"DESC"})),
                        new KeyValuePair<string, StringValues>("BirthDate.Dir", new StringValues(new[] {"ASC"})),
                        new KeyValuePair<string, StringValues>("Sort", new StringValues(new[] {"Name", "BirthDate"}))
                    }
                ));
            var pageableModelBinder = new PageableModelBinder<Customer>(new AutumnSettings());
            var expected = pageableModelBinder.Build(queryColletion);

            Assert.True(expected.PageNumber == 2);
            Assert.True(expected.PageSize == 1);
            var order = new List<Expression<Func<Customer, object>>>(expected.Sort.OrderBy);
            Assert.True(order.Count == 1);
            Assert.True(((MemberExpression)((UnaryExpression) order[0].Body).Operand).Member.Name=="BirthDate");
            
            order = new List<Expression<Func<Customer, object>>>(expected.Sort.OrderDescendingBy);
            Assert.True(((MemberExpression)((UnaryExpression) order[0].Body).Operand).Member.Name=="Name");
            Assert.True(order.Count == 1);

        }
    }
}