using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplicationAuth.AuthorizationRequirements;
using WebApplicationAuth.Transformations;

namespace WebApplicationAuth
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
            services.AddRazorPages().AddRazorPagesOptions(c => 
            {
                c.Conventions.AuthorizePage("/Razor/Secret");
                c.Conventions.AuthorizePage("/Razor/Policy", "Claim.DoB");
            });

            services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config => 
            {
                config.Cookie.Name = "Grandmas.Cookie";
                config.LoginPath = "/Login";
                config.AccessDeniedPath = "/AccessDenied";
            });

            services.AddAuthorization(config => {

                //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //var defaultPolicy = defaultAuthBuilder
                //.RequireAuthenticatedUser()
                //.RequireClaim(ClaimTypes.DateOfBirth)
                //.Build();

                //config.DefaultPolicy = defaultPolicy;

                config.AddPolicy("Claim.DoB", policyBuilder => {
                    policyBuilder.ReqireCustomClaim(ClaimTypes.DateOfBirth);
                });
            });

            // for custom requirement
            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

            // for claim transformation
            services.AddScoped<IClaimsTransformation, ClaimsTransformation>();

            //services.AddControllersWithViews(config =>
            //{
            //    var defaultAuthBuilder = new AuthorizationPolicyBuilder();
            //    var defaultAuthPolict = defaultAuthBuilder.RequireAuthenticatedUser().Build();

            //    config.Filters.Add(new AuthorizeFilter(defaultAuthPolict));
            //});
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
                endpoints.MapRazorPages();
            });
        }
    }
}
