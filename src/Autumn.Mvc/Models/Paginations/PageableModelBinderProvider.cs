using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Models.Paginations
{
    public class PageableModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (!context.Metadata.ModelType.IsGenericType ||
                context.Metadata.ModelType.GetGenericTypeDefinition() != typeof(IPageable<>)) return null;
            var entityType = context.Metadata.ModelType.GetGenericArguments()[0];
            var modelbinderType = typeof(PageableModelBinder<>).MakeGenericType(entityType);
            return (IModelBinder) Activator.CreateInstance(modelbinderType);
        }
    }
}