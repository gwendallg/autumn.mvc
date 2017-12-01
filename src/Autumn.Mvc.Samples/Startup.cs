using System;
using Autumn.Mvc.Samples.Models;
using Autumn.Mvc.Samples.Models.Generators;
using Foundation.ObjectHydrator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Autumn.Mvc.Samples
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // mock data
            var random = new Random();
            var customerHydrator = new Hydrator<Customer>()
                .With(x => x.Address, new Hydrator<Address>())
                .With(x => x.BirthDate, new BirthDateGenerator())
                .With(x => x.Active, new ActiveGenerator());
            var customers = customerHydrator.GetList(1000);
            services.AddSingleton(customers);
            
            
            // autumn.mvc configuration
            services
                .AddAutumn(options=>
                    options
                        // ?query=  Query => query ( snake_case )
                        .QueryFieldName("Query")
                        // snake_case
                        .NamingStrategy(new SnakeCaseNamingStrategy()))
                .AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}