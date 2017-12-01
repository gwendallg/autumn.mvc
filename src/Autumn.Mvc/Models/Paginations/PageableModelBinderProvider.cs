using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Models.Paginations
{
    public class PageableModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (!context.Metadata.ModelType.IsGenericType ||
                context.Metadata.ModelType.GetGenericTypeDefinition() != typeof(IPageable<>)) return null;
            return ModelHelper.GetPageableModelBinder(context.Metadata.ModelType);
        }
    }
}