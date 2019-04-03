using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySvc
{
    public static class Config
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        /// <summary>
        /// 注册api
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetResource()
        {
            return new List<ApiResource>(){
                new ApiResource("api","my api")
            };
        }

        /// <summary>
        /// 注册客户端模式
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>(){
                new Client(){
                    ClientId="client",
                    //客户端模式
                     AllowedGrantTypes=GrantTypes.ClientCredentials,
                     ClientSecrets={new Secret("secret".Sha256())},
                     AllowedScopes={"api"}
                },
                new Client(){
                    ClientId="pwdClient",
                    //OAuth密码模式
                     AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                     ClientSecrets={new Secret("secret".Sha256())},
                     AllowedScopes={"api"}
                },
                new Client
                {
                   ClientId = "mvc",
                   ClientName = "MVC Client",
                   AllowedGrantTypes = GrantTypes.Hybrid,
                   ClientSecrets =
                   {
                       new Secret("secret".Sha256())
                   },
                   // where to redirect to after login
                   RedirectUris = { "http://localhost:5001/signin-oidc" },
                   RequireConsent = false,
                   AllowOfflineAccess = true,
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

                     AllowedScopes = new List<string>
                  {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                  }
                }
            };
        }



        public static List<TestUser> GetTestUser()
        {
            return new List<TestUser>() {
                new TestUser(){
                    SubjectId = "1",
                    Username ="zps",
                    Password = "zps",
                    Claims = new List<Claim>(){
                        new Claim("role","zps"),
                        new Claim("aaa","asdasdsd")
                    }
                },
                 new TestUser(){
                    SubjectId = "2",
                    Username ="admin",
                    Password = "admin",
                     Claims = new List<Claim>(){
                        new Claim("role","admin")
                    }
                }
            };
        }
    }
}
