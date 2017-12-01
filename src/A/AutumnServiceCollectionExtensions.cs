using System;
using Autumn.Mvc.Configurations;
using Autumn.Mvc.Models.Paginations;
using Autumn.Mvc.Models.Queries;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc
{
    public static class AutumnServiceCollectionExtensions
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
            if (autumnOptionsAction == null)
                throw new ArgumentNullException(nameof(autumnOptionsAction));
   
            var autumnConfigurationBuilder = new AutumnOptionsBuilder();
            if (autumnOptionsAction != null)
            {
                autumnOptionsAction(autumnConfigurationBuilder);
            }
            var settings = autumnConfigurationBuilder.Build();
            
            AutumnApplication.Initialize(settings);
            
            var mvcBuilder = services.AddMvc(c =>
            {
                c.ModelBinderProviders.Insert(0,
                    new AutumnPageableModelBinderProvider());
                c.ModelBinderProviders.Insert(1,
                    new AutumnQueryModelBinderProvider());
            });

            var contractResolver =
                new DefaultContractResolver() {NamingStrategy = AutumnApplication.Current.NamingStrategy};
            mvcBuilder.AddJsonOptions(o => { o.SerializerSettings.ContractResolver = contractResolver; });

            return services;
        }

    }
}