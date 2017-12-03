using System;
using System.Net;
using System.Threading.Tasks;
using Autumn.Mvc.Configurations;
using Autumn.Mvc.Models;
using Autumn.Mvc.Models.Queries.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ErrorHandlingMiddleware(RequestDelegate next, AutumnSettings autumnSettings)
        {
            _next = next;
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver =
                    new DefaultContractResolver() {NamingStrategy = autumnSettings.NamingStrategy}
            };
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var result = new ErrorModel() {Message = ex.Message, StackTrace = ex.StackTrace};
                if (ex is QueryComparisonException comparisonException)
                {
                    result.Origin = comparisonException.Origin.GetText();
                }
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result, _jsonSerializerSettings));
            }
        }
    }
}