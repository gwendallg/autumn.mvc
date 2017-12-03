
using System.Collections.Generic;
using Autumn.Mvc.Models.Paginations;
using Moq;
using Xunit;

namespace Autumn.Mvc.Tests.Models.Paginations
{
    public class PageTest
    {
        [Fact]
        public void IsEmptyPage()
        {
            var expected = new Page<object>(null);
            Assert.False(expected.HasContent);
            Assert.False(expected.HasPrevious);
            Assert.False(expected.HasNext);
            Assert.Equal(expected.Number,0);
            Assert.Equal(expected.NumberOfElements,0);
            Assert.False(expected.IsFirst);
            Assert.False(expected.IsLast);
            Assert.Equal(expected.TotalElements,0);
            Assert.Equal(expected.TotalPages,0);
            Assert.NotNull(expected.Content);
            Assert.Equal(expected.Content.Count,0);
        }

        [Fact]
        public void HasContentAndIsFirstPage()
        {
            var mockPagebable = new Mock<IPageable<object>>();
            mockPagebable.Setup(x => x.PageNumber).Returns(0);
            mockPagebable.Setup(x => x.PageSize).Returns(2);
            
            var expected = new Page<object>(new List<object>(new object[] {"1", "2"}),mockPagebable.Object, 200);

            
            Assert.True(expected.HasContent);
            Assert.Equal(expected.Number, 0);
            Assert.False(expected.HasPrevious);
            Assert.True(expected.HasNext);
            Assert.True(expected.IsFirst);
            Assert.False(expected.IsLast);
            Assert.Equal(expected.TotalElements, 200);
            Assert.Equal(expected.NumberOfElements, 2);
            Assert.Equal(expected.TotalPages, 100);
        }

        [Fact]
        public void HasContentAndIsLastPage()
        {
            var mockPagebable = new Mock<IPageable<object>>();
            mockPagebable.Setup(x => x.PageNumber).Returns(199);
            mockPagebable.Setup(x => x.PageSize).Returns(2);
            
            var expected = new Page<object>(new List<object>(new object[] {"1", "2"}),mockPagebable.Object, 200);

            Assert.True(expected.HasContent);
            Assert.Equal(expected.Number, 199);
            Assert.True(expected.HasPrevious);
            Assert.False(expected.HasNext);
            Assert.False(expected.IsFirst);
            Assert.True(expected.IsLast);
            Assert.Equal(expected.TotalElements, 200);
            Assert.Equal(expected.NumberOfElements, 2);
            Assert.Equal(expected.TotalPages, 100);
        }
        
        
        [Fact]
        public void HasContentAndHasPreviousAndHasNext()
        {

            var mockPagebable = new Mock<IPageable<object>>();
            mockPagebable.Setup(x => x.PageNumber).Returns(50);
            mockPagebable.Setup(x => x.PageSize).Returns(2);
            
            var expected = new Page<object>(new List<object>(new object[] {"1", "2"}),mockPagebable.Object, 200);

            Assert.True(expected.HasContent);
            Assert.Equal(expected.Number, 50);
            Assert.True(expected.HasPrevious);
            Assert.True(expected.HasNext);
            Assert.False(expected.IsFirst);
            Assert.False(expected.IsLast);
            Assert.Equal(expected.TotalElements, 200);
            Assert.Equal(expected.NumberOfElements, 2);
            Assert.Equal(expected.TotalPages, 100);
        }
        
    }
}