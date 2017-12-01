using System;
using Autumn.Mvc.Configurations;
using Autumn.Mvc.Models.Paginations;
using Autumn.Mvc.Models.Queries;
using Microsoft.Extensions.DependencyInjection;
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
            Action<AutumnOptionsBuilder> autumnOptionsAction = null)
        {

            if (services == null)
                throw new ArgumentNullException(nameof(services));
          
            var autumnConfigurationBuilder = new AutumnOptionsBuilder();
            autumnOptionsAction?.Invoke(autumnConfigurationBuilder);
            var settings = autumnConfigurationBuilder.Build();
            
            AutumnApplication.Initialize(settings);
            
            var mvcBuilder = services.AddMvc(c =>
            {
                c.ModelBinderProviders.Insert(0,
                    new PageableModelBinderProvider());
                c.ModelBinderProviders.Insert(1,
                    new QueryModelBinderProvider());
            });

            var contractResolver =
                new DefaultContractResolver() {NamingStrategy = AutumnApplication.Current.NamingStrategy};
            mvcBuilder.AddJsonOptions(o => { o.SerializerSettings.ContractResolver = contractResolver; });

            return services;
        }

    }
}