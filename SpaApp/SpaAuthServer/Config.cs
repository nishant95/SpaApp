using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Test;
using System;

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
                        },
                        new Scope
                        {
                            Name = "spa.admin",
                            DisplayName = "Scope for the spa Admin",
                            UserClaims= { "spa.admin" }
                        },
                        new Scope
                        {
                            Name = "spa.user",
                            DisplayName = "Scope for the spa User",
                            UserClaims= {  "spa.user" }
                        }
                    }
                    //UserClaims = { "role", "admin", "user", "spaApi" }
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
                        "http://localhost:44315/swagger",
                        "https://localhost:44315/swagger",
                        "http://localhost:44315/swagger/o2c.html",
                        "https://localhost:44315/swagger/o2c.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:44315/swagger"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44315/",
                        "http://localhost:44315/"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "spaApi",
                        "spaScope",
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
                    ClientName = "angular2client",
                    ClientId = "angular2client",
                    AccessTokenType = AccessTokenType.Reference,
                    //AccessTokenLifetime = 3600, // in seconds, default 60 minutes uncomment to change
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:65035/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:65035/logout"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:65035/",
                        "https://localhost:44335/"
                    },
                    AllowedScopes = new List<string>
                    {
                       "openid",
                        "spaApi",
                        "spaScope",
                        "role"
                    }
                },
                #endregion

                #region Postman
                new Client
                {
                    //RequireConsent = false,
                    ClientName = "postman",
                    ClientId = "postman",
                    AccessTokenType = AccessTokenType.Reference,
                    //AccessTokenLifetime = 600, // 10 minutes, default 60 minutes
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowAccessTokensViaBrowser = true,
                    //ClientSecrets = new List<Secret>
                    //{
                    //    new Secret("swaggersecret".Sha256())
                    //},
                    RedirectUris = new List<string>
                    {
                        "https://www.getpostman.com/oauth2/callback",
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "*"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "spaApi",
                        "spaScope",
                        "role"
                    }
                }
                #endregion
            };
        }

        #endregion

        //Not in use (For testing only)
        #region TestUsers
        //internal static List<TestUser> GetUsers()
        //{
        //    return new List<TestUser>
        //    {
        //        new TestUser
        //        {
        //            Username="qwerty",
        //            Password="qwerty",
        //            Claims=new Claim[] {
        //                new Claim("name","qwerty"),
        //                new Claim("email", "qwerty@gmail.com" ),
        //                new Claim("role", "spa.user" ),
        //                new Claim("role", "spaApi")
        //            }

        //        },
        //        new TestUser
        //        {
        //            Username="asdfg",
        //            Password="asdfg",
        //            Claims=new Claim[] {
        //                new Claim("name","asdfg"),
        //                new Claim("email", "asdfg@gmail.com" ),
        //                new Claim("role", "spa.admin" ),
        //                new Claim("role", "spaApi")
        //            }
        //        }
        //    };
        //}
        #endregion
    }
}
