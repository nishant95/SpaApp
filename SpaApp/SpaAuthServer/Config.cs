using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Test;
using System;

namespace SpaAuthServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("spaScope", new []{ "role", "admin", "user", "spa", "spa.admin" , "spa.user" } )
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("spaApi")
                {
                    ApiSecrets =
                    {
                        new Secret("spaApiSecret".Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "spaScope",
                            DisplayName = "Scope for the spa ApiResource"
                        }
                    },
                    UserClaims = { "role", "admin", "user", "spaApi", "spa.admin", "spa.user" }
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    RequireConsent = false,
                    ClientName = "swaggerclient",
                    ClientId = "swaggerclient",
                    AccessTokenType = AccessTokenType.Reference,
                    //AccessTokenLifetime = 600, // 10 minutes, default 60 minutes
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    //ClientSecrets = new List<Secret>
                    //{
                    //    new Secret("swaggersecret".Sha256())
                    //},
                    RedirectUris = new List<string>
                    {
                        "http://localhost:49616/swagger",
                        "http://localhost:49616/swagger/o2c.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:49616/swagger"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:49616/",
                        "http://localhost:49616/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "spaApi",
                        "spaScope",
                        "role"
                    }
                },
                new Client
                {
                    RequireConsent = false,
                    ClientName = "angularclient",
                    ClientId = "angularclient",
                    AccessTokenType = AccessTokenType.Reference,
                    //AccessTokenLifetime = 600, // 10 minutes, default 60 minutes
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://www.getpostman.com/oauth2/callback",
                        "http://localhost:49616/swagger",
                        "http://localhost:49616/swagger/o2c.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:49616/logout"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:49616/",
                        "http://localhost:49616/"
                    },
                    AllowedScopes = new List<string>
                    {
                       "openid",
                        "spaApi",
                        "spaScope",
                        "role"
                    }
                }
            };
        }

        internal static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    Username="qwerty",
                    Password="qwerty",
                    Claims=new Claim[] {
                        new Claim("name","qwerty"),
                        new Claim("email", "qwerty@gmail.com" ),
                        new Claim("role", "spa.user" ),
                        new Claim("role", "spaApi")
                    }

                },
                new TestUser
                {
                    Username="asdfg",
                    Password="asdfg",
                    Claims=new Claim[] {
                        new Claim("name","asdfg"),
                        new Claim("email", "asdfg@gmail.com" ),
                        new Claim("role", "spa.admin" ),
                        new Claim("role", "spaApi")
                    }
                }
            };
        }
    }
}
