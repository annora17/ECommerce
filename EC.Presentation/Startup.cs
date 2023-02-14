using EC.Infrastructure.EFCore;
using EC.Infrastructure.EFCore.DbContexts;
using EC.Infrastructure.Identity.Models;
using EC.Infrastructure.Extensions;
using EC.Presentation.Filters;
using EC.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;



namespace EC.Presentation
{
    public class Startup
    {
        private readonly StartupEFCore _startupEFCore;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _startupEFCore = new StartupEFCore(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _startupEFCore.ConfigureServices(services);

            //services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"));


            services.AddIdentity<ECUser, ECRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<ECContext>().AddDefaultTokenProviders();

            services.AddRolesAsync();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
                options.HttpOnly = HttpOnlyPolicy.Always;
                //options.ConsentCookie = new CookieBuilder
                //{
                //    Path = "/test",
                //    SameSite = SameSiteMode.Strict
                //};
            });


            //services.AddAuthorization(options =>
            //{
            //    // By default, all incoming requests will be authorized according to the default policy
            //    options.FallbackPolicy = options.DefaultPolicy;
            //});

            


            services.AddRazorPages()
                .AddMvcOptions(options => { })
                .AddMicrosoftIdentityUI();


            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie();
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



            //app.Use(async (context, next) =>
            //{

            //    //if (!context.Request.Headers.TryGetValue("X-Test", out StringValues strs))
            //    //{
            //    //    context.Request.Headers.Add("X-Test", new StringValues(Guid.NewGuid().ToString()));
            //    //}
            //    var CorrelationIdHeaderKey = "X-Test";
            //    //context.Request.Headers.Add("X-Test", new StringValues(Guid.NewGuid().ToString()));
            //    //context.Response.Headers.Add("X-Test-Response", new StringValues(Guid.NewGuid().ToString()));

            //    string correlationId = null;
            //    if (context.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out StringValues correlationIds))
            //    {
            //        correlationId = correlationIds.FirstOrDefault(k => k.Equals(CorrelationIdHeaderKey));
            //        Debug.WriteLine($"NEW CORR ID = {correlationId}");
            //    }
            //    else
            //    {
            //        correlationId = Guid.NewGuid().ToString();
            //        context.Request.Headers.Add(CorrelationIdHeaderKey, correlationId);
            //        Debug.WriteLine($"NEW CORR ID = {correlationId}");
            //    }
            //    context.Response.OnStarting(() =>
            //    {
            //        if (!context.Response.Headers.TryGetValue(CorrelationIdHeaderKey, out correlationIds))
            //            context.Response.Headers.Add(CorrelationIdHeaderKey, correlationId);
            //        return Task.CompletedTask;
            //    });



            //    //context.Response.Headers.Add("X-Test-Response", Guid.NewGuid().ToString());
            //    await next.Invoke();
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseRouting();

            //var cb = new Microsoft.AspNetCore.Http.CookieBuilder
            //{
            //    IsEssential = true,
            //    SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest,
            //    Name = ".ecommerce",
            //    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax,
            //    Domain = ".ecommerce"
            //};


            //var co = new CookiePolicyOptions()
            //{
            //    ConsentCookie = cb
            //};

            //co.ad
            app.UseCookiePolicy();
            app.UseCorrelationId();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
