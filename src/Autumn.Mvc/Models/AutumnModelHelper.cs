using System;
using System.Collections.Generic;
using Autumn.Mvc.Models.Paginations;
using Autumn.Mvc.Models.Queries;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Autumn.Mvc.Models
{
    public static class AutumnModelHelper
    {
        private static readonly Dictionary<Type, IModelBinder> ExpressionModelBinders =
            new Dictionary<Type, IModelBinder>();

        private static readonly Dictionary<Type, IModelBinder> PageableModelBinders =
            new Dictionary<Type, IModelBinder>();


        #region GetExpressionModelBinder

        public static IModelBinder GetExpressionModelBinder(Type type)
        {
            lock (ExpressionModelBinders)
            {
                if (ExpressionModelBinders.ContainsKey(type)) return ExpressionModelBinders[type];
                var entityType = type
                    .GetGenericArguments()[0]
                    .GetGenericArguments()[0];
                var modelBinderType = typeof(AutumnQueryModelBinder<>).MakeGenericType(entityType);
                ExpressionModelBinders.Add(type,(IModelBinder)Activator.CreateInstance(modelBinderType));
                return ExpressionModelBinders[type];
            }
        }

        #endregion

        #region GetPageableModelBinder

        public static IModelBinder GetPageableModelBinder(Type type)
        {
            lock (PageableModelBinders)
            {
                if (PageableModelBinders.ContainsKey(type)) return PageableModelBinders[type];
                var entityType = type
                    .GetGenericArguments()[0];
                var modelBinderType = typeof(AutumnPageableModelBinder<>).MakeGenericType(entityType);
                PageableModelBinders.Add(type,(IModelBinder) Activator.CreateInstance(modelBinderType));
                return PageableModelBinders[type];
            }
        }

        #endregion
    }
}