using Autumn.Mvc.Configurations;
using Microsoft.AspNetCore.Builder;

namespace Autumn.Mvc
{
    public static class ApplicationBuilderExtensions
    {
        public static AutumnSettings GetAutumnSettings(this IApplicationBuilder app)
        {
            return (AutumnSettings) app.ApplicationServices.GetService(typeof(AutumnSettings));
        }
    }
}