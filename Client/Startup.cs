using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
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
            services.AddAuthentication(conf => 
            {
                // We check the cookie to confirm that we are authenticated
                conf.DefaultAuthenticateScheme = "ClientCookie";

                // When we sign in we will deal out a cookie
                conf.DefaultSignInScheme = "ClientCookie";

                // use this to check if we are allowed to do something.
                conf.DefaultChallengeScheme = "OurServer";
            })
            .AddCookie("ClientCookie")
            .AddOAuth("OurServer", conf => 
            {
                conf.CallbackPath = "/oauth/callback";
                conf.ClientId = "client_id";
                conf.ClientSecret = "client_secret";
                conf.AuthorizationEndpoint = "https://localhost:5001/oauth/authorize";
                conf.TokenEndpoint = "https://localhost:5001/oauth/token";
            });

            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
