using System;
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

            var result = app;
            result = result
                .UseMiddleware(typeof(ErrorHandlingMiddleware));
            return result;
        }
    }
}