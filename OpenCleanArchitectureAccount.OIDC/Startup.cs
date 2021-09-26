using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OpenCleanArchitectureAccount.OIDC.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.OIDC
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
            IIdentityServerBuilder serverBuilder = services.AddIdentityServer()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddCustomUserStore();
#if !DEBUG
            //create a signing key
            byte[] pemBytes = Convert.FromBase64String("MHcCAQEEIB2EbKgBGbRxWTtWheDgaNw3P7TsSsMoWloU4NHO3MWYoAoGCCqGSM49AwEHoUQDQgAEVGVVEnzMZnTv/8Jk0/WlFs9poYA7XqI7ITHH78OPenhGS02GBjXMWV/akdaWBgIyUP8/86kJ2KRyuHR4c/jIuA==");
            ECDsa ecdsa = ECDsa.Create();
            ecdsa.ImportECPrivateKey(pemBytes, out _);
            ECDsaSecurityKey securityKey = new ECDsaSecurityKey(ecdsa) { KeyId = "ef208a01ef43406f833b267023766550" };
            serverBuilder.AddSigningCredential(securityKey, IdentityServerConstants.ECDsaSigningAlgorithm.ES256);
            serverBuilder.AddSigningCredential(new RsaSecurityKey(RSA.Create()), IdentityServerConstants.RsaSigningAlgorithm.RS256);
#else
            serverBuilder.AddDeveloperSigningCredential();
#endif

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("<a href='.well-known/openid-configuration'>Discovery document</a>");
                //});
            });
        }
    }
}
