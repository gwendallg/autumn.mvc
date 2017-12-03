using System;
using System.Linq.Expressions;
using Autumn.Mvc.Configurations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Models.Queries
{
    public class QueryModelBinderProvider : IModelBinderProvider
    {

        private readonly AutumnSettings _autumnSettings;
        
        public QueryModelBinderProvider(AutumnSettings autumnSettings)
        {
            _autumnSettings = autumnSettings ?? throw new ArgumentNullException(nameof(autumnSettings));
        }
        
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (!context.Metadata.ModelType.IsGenericType ||
                context.Metadata.ModelType.GetGenericTypeDefinition() != typeof(Expression<>)) return null;
            var entityType = context.Metadata.ModelType.GetGenericArguments()[0].GetGenericArguments()[0];
            var modelbinderType = typeof(QueryModelBinder<>).MakeGenericType(entityType);
            return (IModelBinder) Activator.CreateInstance(modelbinderType,_autumnSettings);
        }
    }
}