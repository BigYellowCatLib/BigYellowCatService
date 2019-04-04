using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using IdentitySvc.Extensions;

namespace IdentitySvc
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
            services.AddMvc();
            var mysql = Configuration.GetConnectionString("mysql");
            services.AddDbContext<IdentityDB>(o => o.UseMySQL(mysql));

            services.AddIdentityServer(o =>
            {
                o.UserInteraction.LoginUrl = "/Auth/Login";
                o.UserInteraction.LogoutUrl = "/Auth/Logout";
                o.UserInteraction.ErrorUrl = "/Auth/Error";
            })
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddDeveloperSigningCredential()
                    .AddInMemoryClients(Config.GetClients())
                    .AddInMemoryApiResources(Config.GetResource())
                    .AddTestUsers(Config.GetTestUser());
            //.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            Config.ServiceProvider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<Injection>();
            app.UseIdentityServer();
            app.UseMvc();
            //MVC配置
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
