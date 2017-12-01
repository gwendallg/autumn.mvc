using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc.Models.Queries
{
    public static class AutumnQueryReflectionHelper
    {
        public static readonly MethodInfo MethodStringContains =
            typeof(string).GetMethod("Contains", new[] {typeof(string)});

        public static readonly MethodInfo MethodStringStartsWith =
            typeof(string).GetMethod("StartsWith", new[] {typeof(string)});

        public static readonly MethodInfo MethodStringEndsWith =
            typeof(string).GetMethod("EndsWith", new[] {typeof(string)});

        private static readonly Dictionary<Type, AutumnMethodContainsInfo> MethodListContains =
            new Dictionary<Type, AutumnMethodContainsInfo>();

        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> MappingJson2PropertyInfo =
            new Dictionary<Type, Dictionary<string, PropertyInfo>>();


        private static Dictionary<string, PropertyInfo> Build(IReflect type, NamingStrategy namingStrategy = null)
        {
            var result = new Dictionary<string, PropertyInfo>();
            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var jsonExclude = property.GetCustomAttribute<JsonIgnoreAttribute>();
                if (jsonExclude != null) continue;
                var jsonPropertyName = GetJsonPropertyName(property, namingStrategy);
                result.Add(jsonPropertyName, property);
            }
            return result;
        }


        private static string GetJsonPropertyName(MemberInfo propertyInfo,
            NamingStrategy namingStrategy = null)
        {
            var propertyName = propertyInfo.Name;
            var attribute = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
            if (attribute != null)
            {
                propertyName = attribute.PropertyName;
            }
            if (namingStrategy == null) return propertyName;
            switch (namingStrategy)
            {
                case SnakeCaseNamingStrategy _:
                    propertyName = propertyName.ToSnakeCase();
                    break;

                case CamelCaseNamingStrategy _:
                    propertyName = propertyName.ToCamelCase();
                    break;
            }
            return propertyName;
        }

        public static PropertyInfo GetOrRegistryProperty(Type type, string name, NamingStrategy namingStrategy = null)
        {
            lock (MappingJson2PropertyInfo)
            {
                if (MappingJson2PropertyInfo.ContainsKey(type)) return MappingJson2PropertyInfo[type][name];
                MappingJson2PropertyInfo[type] = Build(type, namingStrategy);
                return MappingJson2PropertyInfo[type].ContainsKey(name) ? MappingJson2PropertyInfo[type][name] : null;
            }
        }
        
        /// <summary>
        /// find Contains méthode
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static AutumnMethodContainsInfo GetOrRegistryContainsMethodInfo(Type type)
        {
            lock (MethodListContains)
            {
                if (!MethodListContains.ContainsKey(type))
                {
                    MethodListContains.Add(type, new AutumnMethodContainsInfo(type));
                }
                return MethodListContains[type];
            }
        }
        
    }
}