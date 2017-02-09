using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpaData.Context;
using SpaData;
using SpaApi.Services;
using AutoMapper;
using Swashbuckle.Swagger.Model;

namespace SpaApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddCors();
            services.AddAutoMapper();
            services.AddDbContext<SpaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SpaDatabase"), option=>option.MigrationsAssembly("SpaApi")));

            // DI configuration for services.
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPersonService, PersonService>();

            // Swashbuckle Configuration
            services.AddSwaggerGen(c => {
                c.SingleApiVersion(new Info(){
                    Contact = new Contact() { Email = "nishant.h@mindfiresolutions.com", Name = "Nishant" },
                    Title = "SpaApi Docs",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Allow CORS
            app.UseCors(builder =>
                builder.AllowAnyOrigin());

            app.UseMvc();

            //Swashbuckle Configuration
            app.UseSwagger();
            app.UseSwaggerUi();

            
        }
    }
}
