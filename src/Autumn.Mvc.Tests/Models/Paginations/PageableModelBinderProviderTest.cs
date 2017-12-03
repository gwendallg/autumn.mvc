using Autumn.Mvc.Configurations;
using Autumn.Mvc.Models.Paginations;
using Xunit;

namespace Autumn.Mvc.Tests.Models.Paginations
{
    public class PageableModelBinderProviderTest
    {
        [Fact]
        public void IsNotPageableModelBinder()
        {
            var modelBinderProviderContextMock = new ModelBinderProviderContextMock(typeof(string));
            var expected = new PageableModelBinderProvider(new AutumnSettings());
            Assert.Null(expected.GetBinder(modelBinderProviderContextMock));
        }
        
        [Fact]
        public void IsPageableModelBinder()
        {
            var modelBinderProviderContextMock = new ModelBinderProviderContextMock(typeof(IPageable<string>));
            var expected = new PageableModelBinderProvider(new AutumnSettings());
            Assert.NotNull(expected.GetBinder(modelBinderProviderContextMock));
            
            modelBinderProviderContextMock = new ModelBinderProviderContextMock(typeof(Pageable<string>));
            Assert.NotNull(expected.GetBinder(modelBinderProviderContextMock));
        }
    }

}