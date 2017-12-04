using System;
using Autumn.Mvc.Samples.Models;
using Autumn.Mvc.Samples.Models.Generators;
using Autumn.Mvc.Samples.Swagger;
using Foundation.ObjectHydrator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Autumn.Mvc.Samples
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get;}

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
                .AddAutumn(options =>
                    options
                        // ?query=  Query => query ( snake_case )
                        .QueryFieldName("Search")
                        // snake_case
                        .NamingStrategy(new SnakeCaseNamingStrategy())
                        .HostingEnvironment(HostingEnvironment))
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info {Title = "api", Version = "v1"});
                    c.OperationFilter<SwaggerOperationFilter>();
                })
                .AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(string.Format("/swagger/{0}/swagger.json", "v1"),
                        string.Format("API {0}", "v1"));

                })
                .UseMvc();
        }
    }
}