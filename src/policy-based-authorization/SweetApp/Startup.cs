using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SweetApp.Authorization;
using SweetApp.Authorization.Handlers;
using SweetApp.Authorization.Interfaces;
using SweetApp.Contexts;
using SweetApp.Contexts.Interfaces;

namespace SweetApp
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
            // Workaround for this: https://github.com/dotnet/aspnetcore/issues/7644 / https://github.com/dotnet/aspnetcore/issues/8302 / https://stackoverflow.com/questions/47735133/asp-net-core-synchronous-operations-are-disallowed-call-writeasync-or-set-all
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers();

            // Authorization DI
            services.AddSingleton<IAuthorizationRequirementMapper, AuthorizationRequirementMapper>();
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

            services.AddScoped<IAuthorizationHandler, FallbackRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, ApiKeyRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, DefaultPolicyRequirementHandler>();

            // Contexts DI
            services.AddHttpContextAccessor();
            services.AddScoped<IGeneralRequestContext, GeneralRequestContext>();

            // Dummy cookie authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/account/denied";
                    options.LoginPath = "/account/signin";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
