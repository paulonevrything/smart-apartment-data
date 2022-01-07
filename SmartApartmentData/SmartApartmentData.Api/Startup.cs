using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nest;
using SmartApartmentData.Core.Services;
using SmartApartmentData.Core.Services.Interfaces;
using SmartApartmentData.Domain;
using SmartApartmentData.Persistence.Repository;
using SmartApartmentData.Persistence.Repository.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace SmartApartmentData.Api
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
            services.AddControllers();

            // Add singleton Elastic client
            services.AddSingleton<IElasticClient>(OpenSearchConfiguration.GetClient());


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Smart Apartment Data API",
                    Description = "An ASP.NET Core Web API for inplementing AWS OpenSearch",
                    Contact = new OpenApiContact
                    {
                        Name = "Paul Olabisi",
                        Url = new Uri("https://paulonevryth.i.ng")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });

            services.AddTransient<IOpenSearchRepository, OpenSearchRepository>();
            services.AddTransient<ISearchService, SearchService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
