using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.OIDC
{
    /// <summary>
    /// Class to define the resources
    /// </summary>
    public static class Config
    {
        const string Name = "apidemo";

        /// <summary>
        /// Definition for the scopes to protect the resources
        /// </summary>
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope($"{Name}.read", "Read Weather API"),
                new ApiScope($"{Name}.write", "Write Weather API"),
            };

        /// <summary>
        /// Definition the OpenId resources to protect the resources. Identity Token setup
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources => 
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "User roles", new string[] { "role" })
            };

        /// <summary>
        /// Define the API resources to protect. Access token setup
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource($"api://{Name}", "WEB API protected", new[] { "role" })
                {
                    Scopes = { $"{Name}.read", $"{Name}.write" },                    
                }
            };

        /// <summary>
        /// Define the client list can access to the server
        /// </summary>
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {   
                new Client
                {
                    ClientId = "ClientApp",
                    ClientSecrets = { new Secret("Secret".Sha256()) },
                    AllowedScopes = { $"{Name}.read", $"{Name}.write" },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials
                },
                 new Client
                {
                    ClientId = "MVCClientApp",
                    ClientSecrets = { new Secret("Secret".Sha256()) },
                    AllowedScopes = { 
                         IdentityServerConstants.StandardScopes.OpenId, 
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.Email, 
                         IdentityServerConstants.StandardScopes.Address,
                         "roles",
                         $"{Name}.read",
                         $"{Name}.write"  },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"https://localhost:44302/signin-oidc"},
                    PostLogoutRedirectUris= {"https://localhost:44302/signout-callback-oidc"},
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true                           //get refresh token
                },
                 new Client
                {
                    ClientId = "JavaScriptClient",
                    ClientSecrets = { new Secret("Secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:44303/callback.html"},
                    PostLogoutRedirectUris= {"https://localhost:44303/index.html"},
                    AllowedScopes = {
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.Email,
                         IdentityServerConstants.StandardScopes.Address,
                         "roles",
                         $"{Name}.read",
                         $"{Name}.write"  },
                    AllowedCorsOrigins = { 
                        "https://localhost:44303"
                     }
                },
                 new Client
                {
                    ClientId = "BlazorClient",
                    ClientSecrets = { new Secret("Secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:44304/authentication/login-callback"},
                    PostLogoutRedirectUris= {"https://localhost:44304"},
                    AllowedScopes = { 
                         IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.Email,
                         IdentityServerConstants.StandardScopes.Address,
                         "roles",
                         $"{Name}.read",
                         $"{Name}.write"  },
                    AllowedCorsOrigins = {
                        "https://localhost:44304"
                     }
                }
            };

        public static ICollection<string> GrandTypes {
            get
            {
                List<string> test = new List<string>();
                test.AddRange(GrantTypes.ClientCredentials);
                //test.AddRange(GrantTypes.Hybrid);
                test.AddRange(GrantTypes.ResourceOwnerPassword);

                ICollection<string> result = test;

                return result;
            }
        }
    }
}
