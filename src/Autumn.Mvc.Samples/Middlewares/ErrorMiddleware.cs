using Autumn.Mvc.Configurations;
using Autumn.Mvc.Models.Queries.Exceptions;
using Autumn.Mvc.Samples.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Autumn.Mvc.Samples.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AutumnSettings _settings;

        public ErrorMiddleware(RequestDelegate next, AutumnSettings settings)
        {
            _settings = settings;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = new ErrorModel() { Message = exception.Message, StackTrace = exception.StackTrace };
            if (exception is QueryComparisonException comparisonException)
            {
                result.Origin = comparisonException.Origin.GetText();
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result, _settings.JsonSerializerSettings));
        }
    }
}
