using System;
using Autumn.Mvc.Configurations;
using Autumn.Mvc.Models.Paginations;
using Autumn.Mvc.Models.Queries;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// add autumn configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autumnOptionsAction"></param>
        public static IServiceCollection AddAutumn(this IServiceCollection services,
            Action<AutumnSettingsBuilder> autumnOptionsAction = null)
        {

            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var autumnConfigurationBuilder = new AutumnSettingsBuilder();
            autumnOptionsAction?.Invoke(autumnConfigurationBuilder);
            var settings = autumnConfigurationBuilder.Build();
            services.AddSingleton(settings);

            var jsonSerialisation = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = settings.NamingStrategy
                }
            };
            services.AddSingleton(jsonSerialisation);

            var mvcBuilder = services.AddMvc(c =>
            {
                c.ModelBinderProviders.Insert(0,
                    new PageableModelBinderProvider(settings));
                c.ModelBinderProviders.Insert(1,
                    new QueryModelBinderProvider(settings));
            });

           

            if (settings.NamingStrategy == null) return services;
            var contractResolver =
                new DefaultContractResolver() {NamingStrategy = settings.NamingStrategy};
            mvcBuilder.AddJsonOptions(o =>
            {
                o.SerializerSettings.ContractResolver = contractResolver;
                if (settings.HostingEnvironment?.EnvironmentName == "Developement")
                {
                    o.SerializerSettings.Formatting = Formatting.Indented;
                }
            });

            return services;
        }

    }
}