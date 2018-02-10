using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using Autumn.Mvc.Models.Queries.Exceptions;
using System.Threading;

namespace Autumn.Mvc.Models.Queries
{
    public static class QueryGetValueHelper
    {
        static char DecimalSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

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
                items.Add(double.Parse(valueContext.GetText().Replace('.',DecimalSeparator).Replace(',',DecimalSeparator), CultureInfo.InvariantCulture));
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


        public static object GetValue<T>(ParameterExpression parameter, ExpressionValue expressionValue,
            QueryParser.ComparisonContext context,
            NamingStrategy namingStrategy = null)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            if (expressionValue == null) throw new ArgumentNullException(nameof(expressionValue));
            if (context == null) throw new ArgumentNullException(nameof(context));
            
            if (expressionValue.Property.PropertyType == typeof(string))
            {
                return GetString<T>(parameter, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(short) ||
                expressionValue.Property.PropertyType == typeof(short?))
            {
                return GetShort<T>(parameter, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(int) ||
                expressionValue.Property.PropertyType == typeof(int?))
            {
                return GetInt<T>(parameter, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(long) ||
                expressionValue.Property.PropertyType == typeof(long?))
            {
                return GetLong<T>(parameter, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(float) ||
                expressionValue.Property.PropertyType == typeof(float?))
            {
                return GetFloat<T>(parameter, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(double) ||
                expressionValue.Property.PropertyType == typeof(double?))
            {
                return GetDouble<T>(parameter, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(decimal) ||
                expressionValue.Property.PropertyType == typeof(decimal?))
            {
                return GetDecimal<T>(parameter, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(bool) ||
                expressionValue.Property.PropertyType == typeof(bool?))
            {
                return GetBoolean<T>(parameter, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(DateTime) ||
                expressionValue.Property.PropertyType == typeof(DateTime?))
            {
                return GetDateTime<T>(parameter, context.arguments().value()[0], namingStrategy);
            }
            if (expressionValue.Property.PropertyType == typeof(DateTimeOffset) ||
                expressionValue.Property.PropertyType == typeof(DateTimeOffset?))
            {
                return GetDateTimeOffset<T>(parameter, context.arguments().value()[0], namingStrategy);
            }
            return null;
        }

        private static object GetString<T>(ParameterExpression parameter, 
            QueryParser.ValueContext valueContext,
            NamingStrategy namingStrategy = null)
        {
            if (valueContext.single_quote() != null || valueContext.double_quote() != null)
            {
                var replace = valueContext.single_quote() != null ? "'" : "\"";
                var value = valueContext.GetText();
                return value.Length == 2
                    ? string.Empty
                    : value.Substring(1, value.Length - 2).Replace("\\" + replace, replace);
            }
            if (ExpressionValue.TryParse<T>(parameter, valueContext.GetText(), namingStrategy, out var expression))
            {
                return expression;
            }
            return valueContext.GetText();
        }

        private static object GetShort<T>(ParameterExpression parameter, 
            QueryParser.ValueContext valueContext,
            NamingStrategy namingStrategy = null)
        {
            if (short.TryParse(valueContext.GetText(), out var result))
            {
                return result;
            }
            if (ExpressionValue.TryParse<T>(parameter, valueContext.GetText(), namingStrategy, out var expression))
            {
                return expression;
            }
            throw new QueryValueInvalidConversionException(valueContext, typeof(short));
        }

        private static object GetInt<T>(ParameterExpression parameter, QueryParser.ValueContext valueContext,
          NamingStrategy namingStrategy = null)
        {
            if (int.TryParse(valueContext.GetText(), out var result))
            {
                return result;
            }
            if (ExpressionValue.TryParse<T>(parameter, valueContext.GetText(), namingStrategy, out var value))
            {
                return value;
            }
            throw new QueryValueInvalidConversionException(valueContext, typeof(int));
        }

        private static object GetLong<T>(ParameterExpression parameter,
            QueryParser.ValueContext valueContext,
            NamingStrategy namingStrategy = null)
        {
            if (long.TryParse(valueContext.GetText(), out var result))
            {
                return result;
            }
            if (ExpressionValue.TryParse<T>(parameter, valueContext.GetText(), namingStrategy, out var value))
            {
                return value;
            }
            throw new QueryValueInvalidConversionException(valueContext, typeof(long));
        }

        private static object GetFloat<T>(ParameterExpression parameter,
            QueryParser.ValueContext valueContext,
            NamingStrategy namingStrategy = null)
        {
            if (float.TryParse(valueContext.GetText().Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator), out var result))
            {
                return result;
            }
            if (ExpressionValue.TryParse<T>(parameter, valueContext.GetText(), namingStrategy, out var value))
            {
                return value;
            }
            throw new QueryValueInvalidConversionException(valueContext, typeof(float));
        }

        private static object GetDouble<T>(ParameterExpression parameter, QueryParser.ValueContext valueContext,
      NamingStrategy namingStrategy = null)
        {
            if (double.TryParse(valueContext.GetText().Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator), out var result))
            {
                return result;
            }
            if (ExpressionValue.TryParse<T>(parameter, valueContext.GetText(), namingStrategy, out var value))
            {
                return value;
            }
            throw new QueryValueInvalidConversionException(valueContext, typeof(double));
        }

        private static object GetDecimal<T>(ParameterExpression parameter, QueryParser.ValueContext valueContext,
     NamingStrategy namingStrategy = null)
        {
            if (decimal.TryParse(valueContext.GetText().Replace(".", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator), out var result))
            {
                return result;
            }
            if (ExpressionValue.TryParse<T>(parameter, valueContext.GetText(), namingStrategy, out var value))
            {
                return value;
            }
            throw new QueryValueInvalidConversionException(valueContext, typeof(decimal));
        }

        private static object GetBoolean<T>(ParameterExpression parameter, QueryParser.ValueContext valueContext,
    NamingStrategy namingStrategy = null)
        {
            if (bool.TryParse(valueContext.GetText(), out var result))
            {
                return result;
            }
            if (ExpressionValue.TryParse<T>(parameter, valueContext.GetText(), namingStrategy, out var value))
            {
                return value;
            }
            throw new QueryValueInvalidConversionException(valueContext, typeof(bool));
        }

        private static object GetDateTime<T>(ParameterExpression parameter, QueryParser.ValueContext valueContext,
    NamingStrategy namingStrategy = null)
        {
            try
            {
                return DateTime.Parse(valueContext.GetText(), CultureInfo.InvariantCulture,
                     DateTimeStyles.RoundtripKind);
            }
            catch
            {
                if (ExpressionValue.TryParse<T>(parameter, valueContext.GetText(), namingStrategy, out var value))
                {
                    return value;
                }
                throw new QueryValueInvalidConversionException(valueContext, typeof(DateTime));
            }
        }

        private static object GetDateTimeOffset<T>(ParameterExpression parameter, QueryParser.ValueContext valueContext,
    NamingStrategy namingStrategy = null)
        {
            try
            {
                return DateTimeOffset.Parse(valueContext.GetText(), CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);
            }
            catch
            {
                if (ExpressionValue.TryParse<T>(parameter, valueContext.GetText(), namingStrategy, out var value))
                {
                    return value;
                }
                throw new QueryValueInvalidConversionException(valueContext, typeof(DateTimeOffset));
            }
        }

    }
}