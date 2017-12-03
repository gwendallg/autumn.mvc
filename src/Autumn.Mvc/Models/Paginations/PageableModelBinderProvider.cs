using System;
using Autumn.Mvc.Configurations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Models.Paginations
{
    public class PageableModelBinderProvider : IModelBinderProvider
    {
        private readonly AutumnSettings _autumnSettings;

        public PageableModelBinderProvider(AutumnSettings autumnSettings)
        {
            _autumnSettings = autumnSettings ?? throw new ArgumentNullException(nameof(autumnSettings));
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (!context.Metadata.ModelType.IsGenericType ||
                (context.Metadata.ModelType.GetGenericTypeDefinition() != typeof(IPageable<>) &&
                 context.Metadata.ModelType.GetGenericTypeDefinition() != typeof(Pageable<>))) return null;
            var entityType = context.Metadata.ModelType.GetGenericArguments()[0];
            var modelbinderType = typeof(PageableModelBinder<>).MakeGenericType(entityType);
            return (IModelBinder) Activator.CreateInstance(modelbinderType, _autumnSettings);
        }
    }
}