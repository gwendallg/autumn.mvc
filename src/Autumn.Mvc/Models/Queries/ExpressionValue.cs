﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc.Models.Queries
{
    public class ExpressionValue
    {
        public PropertyInfo Property { get; private set; }
        public Expression Expression { get; private set; }

        public static bool TryParse<T>(ParameterExpression parameter,
            string selector, NamingStrategy namingStrategy, out ExpressionValue result
        )
        {
            result = null;
            try
            {
                result = Parse<T>(parameter, selector, namingStrategy);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public static ExpressionValue Parse<T>(ParameterExpression parameter,
            string selector,
            NamingStrategy namingStrategy)
        {
            if (parameter == null) throw new ArgumentException(nameof(parameter));
            if (selector == null) throw new ArgumentException(nameof(selector));
            Expression lastMember = parameter;
            PropertyInfo property = null;
            var type = typeof(T);
            if (selector.IndexOf(".", StringComparison.InvariantCulture) != -1)
            {
                foreach (var item in selector.Split('.'))
                {
                    property = QueryReflectionHelper.GetOrRegistryProperty(type, item, namingStrategy);
                    if (property == null)
                    {
                        throw new Exception(string.Format("Invalid property {0}", selector));
                    }
                    type = property.PropertyType;
                    lastMember = Expression.Property(lastMember, property);
                }
            }
            else
            {
                property = QueryReflectionHelper.GetOrRegistryProperty(type, selector, namingStrategy);
                if (property == null)
                {
                    throw new Exception(string.Format("Invalid property {0}", selector));
                }
                lastMember = Expression.Property(lastMember, property);
            }

            return new ExpressionValue()
            {
                Property = property,
                Expression = lastMember
            };
        }
    }
}