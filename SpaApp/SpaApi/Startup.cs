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
using AutoMapper;
using Newtonsoft.Json.Serialization;
using IdentityServer4.AccessTokenValidation;

using SpaData.Context;
using SpaData;
using SpaApi.Services;
using SpaApi.Swashbuckle;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddCors(options=>
            {
                options.AddPolicy("SpaCorsPolicy",builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("spaAdmin", policyAdmin =>
                {
                    policyAdmin.RequireClaim("role", "spa.admin");
                });
                options.AddPolicy("spaUser", policyUser =>
                {
                    policyUser.RequireClaim("role", "spa.user");
                });
            });

            services.AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver =
                        new DefaultContractResolver();
                });

            services.AddAutoMapper();
            services.AddDbContext<SpaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SpaDatabase"), option=>option.MigrationsAssembly("SpaApi")));

            // DI configuration for services.
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPersonService, PersonService>();

            // Swashbuckle Configuration
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info(){
                    Contact = new Contact() { Email = "nishant.h@mindfiresolutions.com", Name = "Nishant" },
                    Title = "SpaApi Docs",
                    Version = "v1"
                });

                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "http://localhost:54412/Consent",
                    Scopes = new Dictionary<string, string>
                    {
                        { "spa", "Access the Api" }
                    }
                });

                c.OperationFilter<SwaggerAuthFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Allow CORS
            app.UseCors("SpaCorsPolicy");

            IdentityServerAuthenticationOptions identityServerValidationOptions = new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:54412/",
                AllowedScopes = new List<string> { "spa" },
                ApiSecret = "spaSecret",
                ApiName = "spa",
                AutomaticAuthenticate = true,
                SupportedTokens = SupportedTokens.Both,
                // TokenRetriever = _tokenRetriever,
                // required if you want to return a 403 and not a 401 for forbidden responses
                AutomaticChallenge = true,
                RequireHttpsMetadata = false
            };

            app.UseIdentityServerAuthentication(identityServerValidationOptions);

            app.UseMvc();

            //Swashbuckle Configuration
            app.UseSwagger();
            app.UseSwaggerUi(c=> 
            {
                c.ConfigureOAuth2("angular2client", "", "", "angular2client",additionalQueryStringParameters: new {
                    returnUrl = "http://localhost:49616/Account/login"
                });

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}

//ClientName = "angular2client",
//ClientId = "angular2client",
//AccessTokenType = AccessTokenType.Reference,
////AccessTokenLifetime = 600, // 10 minutes, default 60 minutes
//AllowedGrantTypes = GrantTypes.Implicit,
//AllowAccessTokensViaBrowser = true,
//RedirectUris = new List<string>
//{
//    "http://localhost:65035"

//},
//PostLogoutRedirectUris = new List<string>
//{
//    "http://localhost:65035/Unauthorized"
//},
//AllowedCorsOrigins = new List<string>
//{
//    "https://localhost:65035",
//    "http://localhost:65035"
//},
//AllowedScopes = new List<string>
//{
//    "openid",
//    "spa",
//    "spaScope",
//    "role"
//}
