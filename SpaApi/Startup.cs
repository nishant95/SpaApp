#region Namespaces

using System.Collections.Generic;
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
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography.X509Certificates;
using static SpaData.Constant;
#endregion

namespace SpaApi
{
    public class Startup
    {
        #region Privates and Constants

        private const string CorsPolicyName = "SpaCorsPolicy";
        private const string CertificateFile = "idsrv3test.pfx";
        private const string CertificatePassword = "idsrv3test";

        private readonly IHostingEnvironment _env;

        #endregion

        #region Properties

        public IConfigurationRoot Configuration { get; }

        #endregion

        #region Methods

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Code for data encryption (Currently not in use)

            var folderForKeyStore = Configuration["KeyStore"];
            //var cert = new X509Certificate2(
            //    Path.Combine(_env.ContentRootPath, CertificateFile),
            //    CertificatePassword);

            // Important The folderForKeyStore needs to be backed up.
            //services.AddDataProtection()
            //    .SetApplicationName("SpaApi")
            //    .PersistKeysToFileSystem(new DirectoryInfo(folderForKeyStore));
            ////.ProtectKeysWithCertificate(cert);

            #endregion

            #region Cross-Origin Policy

            //Cross-origin requests policy
            services.AddCors(options=>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            #endregion

            #region Setup Auth Logic

            var guestPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("scope", "spaApi")
                .Build();

            // Configuration for Auth Server
            services.AddAuthorization(options =>
            {
                options.AddPolicy("spaAdmin", policyAdmin =>
                {
                    policyAdmin.RequireClaim("role", "spa.admin");
                    //policyAdmin.RequireRole("spa.admin");
                });
                options.AddPolicy("spaUser", policyUser =>
                {
                    //policyUser.RequireRole("spa.user");
                    policyUser.RequireClaim("role", "spa.user");
                });
            });

            #endregion

            #region SwaggerGen Configuration

            // Swashbuckle Configuration
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info()
                {
                    Contact = new Contact()
                    {
                        Email = DeveloperEmailSwagger,
                        Name = DeveloperNameSwagger
                    },
                    Title = TitleSwagger,
                    Version = VersionSwagger
                });

                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = AuthorizeEndpointHttps,
                    TokenUrl = TokenEndpointHttps,
                    Scopes = new Dictionary<string, string>
                    {
                        { "spaApi", "Access the Api" },
                        { "spa.user", "User Access" },
                        { "spa.admin", "Admin Access" }
                    }
                });

                c.OperationFilter<SwaggerAuthFilter>();

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "SpaApi.xml");
                c.IncludeXmlComments(filePath);
            });

            #endregion

            #region MVC, DbContext and Services

            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter(guestPolicy));
            })
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver =
                        new DefaultContractResolver();
                });

            services.AddAutoMapper();
            services.AddDbContext<SpaContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("SpaDatabase"), option => option.MigrationsAssembly("SpaApi"));
            });

            // DI configuration for services.
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPersonService, PersonService>();

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Setup logging
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Allow CORS
            app.UseCors(CorsPolicyName);

            //Disable default mapping for JWT
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //Setup Auth Server
            var identityServerValidationOptions = new IdentityServerAuthenticationOptions
            {
                Authority = AuthAuthorityUriHttps,
                AllowedScopes = new List<string> { "spaApi" }, //,"spaScope", "spa.user","spa.admin"
                ApiSecret = "spaSecret",
                ApiName = "spaApi",
                AutomaticAuthenticate = true,
                SupportedTokens = SupportedTokens.Both,
                // TokenRetriever = _tokenRetriever,
                // required if you want to return a 403 and not a 401 for forbidden responses
                AutomaticChallenge = true,
                //RequireHttpsMetadata = false
            };

            app.UseIdentityServerAuthentication(identityServerValidationOptions);

            

            //Swashbuckle Configuration
            app.UseSwagger();
            app.UseSwaggerUi(c=> 
            {
                c.ConfigureOAuth2(SwaggerClientName, "", "", SwaggerClientName, additionalQueryStringParameters: new {
                    redirect_uri = SwaggerRedirectUriHttps
                });

                c.SwaggerEndpoint(SwaggerEndpoint, SwaggerDescription);
            });

            //Migrate to latest and run seeding logic
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SpaContext>();
                context.Database.Migrate();
                context.EnsureSeedData();
            }

            app.UseMvc();
        }

        #endregion
    }
}
