// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using System.Collections.Generic;

namespace SpaAuthServer
{
    using System.Security.Claims;

    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("spa", new []{ "role", "admin", "user", "spa", "spa.admin" , "spa.user" } )
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("spa")
                {
                    ApiSecrets =
                    {
                        new Secret("spaSecret".Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "spa",
                            DisplayName = "Scope for the spa ApiResource"
                        }
                    },
                    UserClaims = { "role", "admin", "user", "spa", "spa.admin", "spa.user" }
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
                    ClientName = "angular2client",
                    ClientId = "angular2client",
                    AccessTokenType = AccessTokenType.Reference,
                    //AccessTokenLifetime = 600, // 10 minutes, default 60 minutes
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:49616/swagger"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:49616/swagger"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:49616/swagger",
                        "http://localhost:49616/swagger"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "spa",
                        "spaScope",
                        "role"
                    }
                }
            };
        }
    }
}
