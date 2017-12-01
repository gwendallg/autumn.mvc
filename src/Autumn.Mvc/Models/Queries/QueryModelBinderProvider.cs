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
            return ModelHelper.GetExpressionModelBinder(context.Metadata.ModelType);
        }
    }
}