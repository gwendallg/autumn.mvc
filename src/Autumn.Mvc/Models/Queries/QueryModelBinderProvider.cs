using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Models.Queries
{
    public class QueryModelBinderProvider : IModelBinderProvider
    {

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (!context.Metadata.ModelType.IsGenericType ||
                context.Metadata.ModelType.GetGenericTypeDefinition() != typeof(Expression<>)) return null;
            var entityType = context.Metadata.ModelType.GetGenericArguments()[0].GetGenericArguments()[0];
            var modelbinderType = typeof(QueryModelBinder<>).MakeGenericType(entityType);
            return (IModelBinder) Activator.CreateInstance(modelbinderType);
        }
    }
}