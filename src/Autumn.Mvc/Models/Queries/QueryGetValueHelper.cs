using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace Autumn.Mvc.Models.Queries
{
    public static class QueryGetValueHelper
    {
        private static List<object> GetStrings(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                if (valueContext.single_quote() != null || valueContext.double_quote() != null)
                {
                    var replace = valueContext.single_quote() != null ? "'" : "\"";
                    var value = valueContext.GetText();
                    if (value.Length == 2)
                    {
                        items.Add(string.Empty);
                    }
                    items.Add(value.Substring(1, value.Length - 2).Replace("\\" + replace, replace));
                }
                else
                {
                    items.Add(valueContext.GetText());
                }
            }
            return items;
        }

        private static List<object> GetShorts(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(short.Parse(valueContext.GetText()));
            }
            return items;
        }

        private static List<object> GetInts(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(int.Parse(valueContext.GetText()));
            }
            return items;
        }

        private static List<object> GetLongs(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(long.Parse(valueContext.GetText()));
            }
            return items;
        }

        private static List<object> GetDoubles(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(double.Parse(valueContext.GetText(), CultureInfo.InvariantCulture));
            }
            return items;
        }

        private static List<object> GetFloats(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(float.Parse(valueContext.GetText(), CultureInfo.InvariantCulture));
            }
            return items;
        }

        private static List<object> GetDecimals(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(decimal.Parse(valueContext.GetText(), CultureInfo.InvariantCulture));
            }
            return items;
        }

        private static List<object> GetDateTimes(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(DateTime.Parse(valueContext.GetText(), CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind));
            }
            return items;
        }
        
        private static List<object> GetDateTimeOffsets(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(DateTimeOffset.Parse(valueContext.GetText(), CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind));
            }
            return items;
        }
        
        
        private static List<object> GetBooleans(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(bool.Parse(valueContext.GetText()));
            }
            return items;
        }

        private static List<object> GetChars(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(char.Parse(valueContext.GetText()));
            }
            return items;
        }
        
        private static List<object> GetBytes(QueryParser.ArgumentsContext argumentsContext)
        {
            var items = new List<object>();
            foreach (var valueContext in argumentsContext.value())
            {
                items.Add(byte.Parse(valueContext.GetText()));
            }
            return items;
        }

        public static List<object> GetValues(Type type, QueryParser.ArgumentsContext argumentsContext)
        {
            if (argumentsContext?.value() == null || argumentsContext.value().Length == 0) return null;
            if (type == typeof(string))
            {
                return GetStrings(argumentsContext);
            }

            if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return GetDateTimes(argumentsContext);
            }

            if (type == typeof(short) || type == typeof(short?))
            {
                return GetShorts(argumentsContext);
            }

            if (type == typeof(int) || type == typeof(int?))
            {
                return GetInts(argumentsContext);
            }

            if (type == typeof(long) || type == typeof(long?))
            {
                return GetLongs(argumentsContext);
            }

            if (type == typeof(float) || type == typeof(float?))
            {
                return GetFloats(argumentsContext);
            }

            if (type == typeof(double) || type == typeof(double?))
            {
                return GetDoubles(argumentsContext);
            }

            if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return GetDecimals(argumentsContext);
            }

            if (type == typeof(bool) || type == typeof(bool?))
            {
                return GetBooleans(argumentsContext);
            }

            if (type == typeof(char) || type == typeof(char?))
            {
                return GetChars(argumentsContext);
            }

            if (type == typeof(byte) || type == typeof(byte?))
            {
                return GetBytes(argumentsContext);
            }
            
            if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
            {
                return GetDateTimeOffsets(argumentsContext);
            }
            return null;
        }


        public static object GetValue<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ComparisonContext context,
           NamingStrategy namingStrategy = null)
        {
            if (expressionValue.Property.PropertyType == typeof(string))
            {
                return GetString<T>(parameter, expressionValue, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(short) || expressionValue.Property.PropertyType == typeof(short?))
            {
                return GetShort<T>(parameter, expressionValue, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(int) || expressionValue.Property.PropertyType == typeof(int?))
            {
                return GetInt<T>(parameter, expressionValue, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(long) || expressionValue.Property.PropertyType == typeof(long?))
            {
                return GetLong<T>(parameter, expressionValue, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(float) || expressionValue.Property.PropertyType == typeof(float?))
            {
                return GetFloat<T>(parameter, expressionValue, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(double) || expressionValue.Property.PropertyType == typeof(double?))
            {
                return GetDouble<T>(parameter, expressionValue, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(decimal) || expressionValue.Property.PropertyType == typeof(decimal?))
            {
                return GetDecimal<T>(parameter, expressionValue, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(bool) || expressionValue.Property.PropertyType == typeof(bool?))
            {
                return GetBoolean<T>(parameter, expressionValue, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(DateTime) || expressionValue.Property.PropertyType == typeof(DateTime?))
            {
                return GetDateTime<T>(parameter, expressionValue, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(DateTimeOffset) || expressionValue.Property.PropertyType == typeof(DateTimeOffset?))
            {
                return GetDateTimeOffset<T>(parameter, expressionValue, context.arguments().value()[0], namingStrategy);
            }
            return null;
        }

        private static object GetString<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ValueContext valueContext,
            NamingStrategy namingStrategy = null)
        {
            if (valueContext.single_quote() != null || valueContext.double_quote() != null)
            {
                var replace = valueContext.single_quote() != null ? "'" : "\"";
                var value = valueContext.GetText();
                if (value.Length == 2) return string.Empty;
                return value.Substring(1, value.Length - 2).Replace("\\" + replace, replace);
            }
            return QueryExpressionHelper.GetMemberExpressionValue<T>(parameter, valueContext.GetText(), namingStrategy);
        }


        private static object GetShort<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ValueContext valueContext,
            NamingStrategy namingStrategy = null)
        {
            if (short.TryParse(valueContext.GetText(), out var result))
            {
                return result;
            }
            return QueryExpressionHelper.GetMemberExpressionValue<T>(parameter, valueContext.GetText(), namingStrategy);
        }

        private static object GetInt<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ValueContext valueContext,
          NamingStrategy namingStrategy = null)
        {
            if (int.TryParse(valueContext.GetText(), out var result))
            {
                return result;
            }
            return QueryExpressionHelper.GetMemberExpressionValue<T>(parameter, valueContext.GetText(), namingStrategy);
        }

        private static object GetLong<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ValueContext valueContext,
        NamingStrategy namingStrategy = null)
        {
            if (int.TryParse(valueContext.GetText(), out var result))
            {
                return result;
            }
            return QueryExpressionHelper.GetMemberExpressionValue<T>(parameter, valueContext.GetText(), namingStrategy);
        }

        private static object GetFloat<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ValueContext valueContext,
       NamingStrategy namingStrategy = null)
        {
            if (float.TryParse(valueContext.GetText(), NumberStyles.None, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            return QueryExpressionHelper.GetMemberExpressionValue<T>(parameter, valueContext.GetText(), namingStrategy);
        }

        private static object GetDouble<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ValueContext valueContext,
      NamingStrategy namingStrategy = null)
        {
            if (double.TryParse(valueContext.GetText(), NumberStyles.None, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            return QueryExpressionHelper.GetMemberExpressionValue<T>(parameter, valueContext.GetText(), namingStrategy);
        }

        private static object GetDecimal<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ValueContext valueContext,
     NamingStrategy namingStrategy = null)
        {
            if (decimal.TryParse(valueContext.GetText(), NumberStyles.None, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            return QueryExpressionHelper.GetMemberExpressionValue<T>(parameter, valueContext.GetText(), namingStrategy);
        }

        private static object GetBoolean<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ValueContext valueContext,
    NamingStrategy namingStrategy = null)
        {
            if (bool.TryParse(valueContext.GetText(), out var result))
            {
                return result;
            }
            return QueryExpressionHelper.GetMemberExpressionValue<T>(parameter, valueContext.GetText(), namingStrategy);
        }

        private static object GetDateTime<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ValueContext valueContext,
    NamingStrategy namingStrategy = null)
        {
            try
            {
                return DateTime.Parse(valueContext.GetText(), CultureInfo.InvariantCulture,
                     DateTimeStyles.RoundtripKind);
            }
            catch
            {
                return QueryExpressionHelper.GetMemberExpressionValue<T>(parameter, valueContext.GetText(), namingStrategy);
            }
        }

        private static object GetDateTimeOffset<T>(ParameterExpression parameter, ExpressionValue expressionValue, QueryParser.ValueContext valueContext,
    NamingStrategy namingStrategy = null)
        {
            try
            {
                return DateTimeOffset.Parse(valueContext.GetText(), CultureInfo.InvariantCulture,
                     DateTimeStyles.RoundtripKind);
            }
            catch
            {
                return QueryExpressionHelper.GetMemberExpressionValue<T>(parameter, valueContext.GetText(), namingStrategy);
            }
        }

    }
}