using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Autumn.Mvc.Models;
using Autumn.Mvc.Models.Paginations;
using Autumn.Mvc.Samples.Controllers;
using Autumn.Mvc.Samples.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen; 

namespace Autumn.Mvc.Samples.Swagger
{
    public class SwaggerOperationFilter : IOperationFilter
    {

        private const string ConsumeContentType = "application/json";
        private static readonly ConcurrentDictionary<Type,Dictionary<HttpMethod,Schema>> Caches = new ConcurrentDictionary<Type,Dictionary<HttpMethod,Schema>>();
        private static readonly Schema ErrorModelSchema;

        /// <summary>
        /// class initializer
        /// </summary>
        static SwaggerOperationFilter()
        {
            ErrorModelSchema = GetOrRegistrySchema(typeof(ErrorModel), HttpMethod.Get);
        }

        /// <summary>
        /// apply operation description
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) return;
            if (!(context.ApiDescription.ActionDescriptor is ControllerActionDescriptor actionDescriptor)) return;
            if (actionDescriptor.ControllerTypeInfo.AsType() != typeof(CustomerController)) return;
            var entityType = typeof(Customer);
            var entitySchemaGet = GetOrRegistrySchema(entityType, HttpMethod.Get);
            operation.Responses = new ConcurrentDictionary<string, Response>();
            // add generic reponse for internal error from server
            operation.Responses.Add(((int) HttpStatusCode.InternalServerError).ToString(),
                new Response() {Schema = ErrorModelSchema});
            operation.Consumes.Clear();
            if (actionDescriptor.ActionName != "Get") return;
            var genericPageType = typeof(Page<>);
            var pageType = genericPageType.MakeGenericType(entityType);
            var schema = GetOrRegistrySchema(pageType, HttpMethod.Get);
            operation.Responses.Add("200", new Response() {Schema = schema, Description = "OK"});
            operation.Responses.Add("206", new Response() {Schema = schema, Description = "Partial Content"});
            operation.Parameters.Clear();
            operation.Description = "This is sample to use Autumn.mvc";
            operation.Summary = "Autumn.Mvc Samples";
            IParameter parameter = new NonBodyParameter
            {
                Type = "string",
                In = "query",
                Description = "Query to search (cf. http://tools.ietf.org/html/draft-nottingham-atompub-fiql-00)",
                Name = AutumnApplication.Current.QueryFieldName
            };
            operation.Parameters.Add(parameter);

            parameter = new NonBodyParameter
            {
                In = "query",
                Type = "integer",
                Minimum = 0,
                Format = "int32",
                Description = "Size of the page",
                Default = AutumnApplication.Current.DefaultPageSize,
                Name = AutumnApplication.Current.PageSizeFieldName
            };
            operation.Parameters.Add(parameter);

            parameter = new NonBodyParameter
            {
                In = "query",
                Type = "integer",
                Description = "Paging number (start to zero)",
                Minimum = 0,
                Format = "int32",
                Default = 0,
                Name = AutumnApplication.Current.PageNumberFieldName
            };
            operation.Parameters.Add(parameter);
        }

        /// <summary>
        /// build schema 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        private static Schema BuildSchema(PropertyInfo property,HttpMethod httpMethod)
        {
            var result = new Schema();
            if (property.PropertyType == typeof(string))
            {
                result.Type = "string";
            }
            else if (property.PropertyType == typeof(short) ||
                     property.PropertyType == typeof(short?) ||
                     property.PropertyType == typeof(int) ||
                     property.PropertyType == typeof(int?))
            {
                result.Type = "integer";
                result.Format = "int32";
            }
            else if (property.PropertyType == typeof(long) ||
                     property.PropertyType == typeof(long?))
            {
                result.Type = "integer";
                result.Format = "int64";
            }
            else if (property.PropertyType == typeof(decimal) ||
                     property.PropertyType == typeof(decimal?) ||
                     property.PropertyType == typeof(double) ||
                     property.PropertyType == typeof(double?))
            {
                result.Type = "number";
                result.Format = "double";
            }
            else if (property.PropertyType == typeof(DateTime) ||
                     property.PropertyType == typeof(DateTime?))
            {
                result.Type = "string";
                result.Format = "date-time";
            }
            else if (property.PropertyType == typeof(byte) ||
                     property.PropertyType == typeof(byte?))
            {
                result.Type = "string";
                result.Format = "byte";
            }
            else if (property.PropertyType == typeof(bool) ||
                     property.PropertyType == typeof(bool?))
            {
                result.Type = "boolean";
            }
            else
            {
                if (property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(IList<>))
                {
                    result.Type = "array";
                    result.Items = GetOrRegistrySchema(property.PropertyType.GetGenericArguments()[0],httpMethod);
                }
                else if (property.PropertyType.IsArray)
                {
                    result.Type = "array";
                    result.Items = GetOrRegistrySchema(property.PropertyType, httpMethod);
                }
                else
                {
                    result = GetOrRegistrySchema(property.PropertyType, httpMethod);
                }
            }
            return result;
        }

        /// <summary>
        /// register in cache ( if not exist) and return swagger schema for the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static Schema GetOrRegistrySchema(Type type,HttpMethod method)
        {
            lock (Caches)
            {
                if (Caches.ContainsKey(type) && Caches[type].ContainsKey(method)) return Caches[type][method];
                if (!Caches.ContainsKey(type)) Caches[type] = new Dictionary<HttpMethod, Schema>();
                var o = Activator.CreateInstance(type);
                var stringify = JsonConvert.SerializeObject(o);
                var expected = JObject.Parse(stringify);
                var result = new Schema {Properties = new ConcurrentDictionary<string, Schema>()};
                foreach (var propertyName in expected.Properties())
                {
                    var name = AutumnApplication.Current.NamingStrategy.GetPropertyName(propertyName.Name,false);
                    var property = type.GetProperty(propertyName.Name);
                    if (property == null) continue;
                    var propertySchema = BuildSchema(property, method);
                    if (propertySchema != null)
                    {
                        result.Properties.Add(name, propertySchema);
                    }
                }
                Caches[type][method] = result;
                return result;
            }
        }
    }
}