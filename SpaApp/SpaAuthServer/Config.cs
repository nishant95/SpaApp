#region Namespaces
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;
using static SpaData.Constant;
#endregion

namespace SpaAuthServer
{
    public class Config
    {
        #region In-Memory IdentityResources
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("spaScope", new []{ "role", "admin", "user", "spa", "spa.admin" , "spa.user" } )
            };
        }
        #endregion

        #region In-Memory Api-Resources
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("spaApi","SPA Api")
                {
                    ApiSecrets =
                    {
                        new Secret("spaSecret".Sha256())
                    },
                    UserClaims =
                    {
                        "role",
                        "admin",
                        "user",
                        "spaApi",
                        "spa.user",
                        "spa.admin"
                    }
                }
            };
        }
        #endregion

        #region In-Memory Clients
        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials list
            return new List<Client>
            {
                #region Swagger client
                new Client
                {
                    RequireConsent = false,
                    ClientName = SwaggerClientName,
                    ClientId = SwaggerClientId,
                    AccessTokenType = AccessTokenType.Reference,
                    //AccessTokenLifetime = 600, // 10 minutes, default 60 minutes
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        SwaggerHomeUriHttp,
                        SwaggerHomeUriHttps,
                        SwaggerRedirectUriHttp,
                        SwaggerRedirectUriHttps
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        SwaggerHomeUriHttp
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        SpaApiHomeUriHttp,
                        SpaApiHomeUriHttps
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "spaApi",
                        "role",
                        "spa.user",
                        "spa.admin"
                    }
                },
                #endregion

                #region Angular Client
                new Client
                {
                    RequireConsent = false,
                    ClientName = SpaAngularClientName,
                    ClientId = SpaAngularClientId,
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 3600, // in seconds, default 60 minutes uncomment to change
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        SpaAppHomeUriHttp,
                        SpaAppHomeUriHttps
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        SpaAppLogoutRedirectUri
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        SpaAppHomeUriHttp,
                        SpaAppHomeUriHttps
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "spaApi"
                    }
                },
                #endregion

                #region Postman
                new Client
                {
                    //RequireConsent = false,
                    ClientName = PostmanClientName,
                    ClientId = PostmanClientId,
                    AccessTokenType = AccessTokenType.Reference,
                    //AccessTokenLifetime = 600, // 10 minutes, default 60 minutes
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    
                    RedirectUris = new List<string>
                    {
                        "https://www.getpostman.com/oauth2/callback",
                    },
                    PostLogoutRedirectUris = new List<string>(),
                    AllowedCorsOrigins = new List<string>
                    {
                        "*"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "spaApi",
                        "role"
                    }
                }
                #endregion
            };
        }

        #endregion
    }
}
