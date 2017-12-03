using System;
using Autumn.Mvc.Configurations;
using Autumn.Mvc.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Autumn.Mvc
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder UseAutumn(this IApplicationBuilder app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            var settings = (AutumnSettings) app.ApplicationServices.GetService(typeof(AutumnSettings));

            var result = app;
            result = result
                .UseMiddleware(typeof(ErrorHandlingMiddleware), settings);
            return result;
        }
    }
}