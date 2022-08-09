using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application;
using AccountManagement.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using ShopManagement.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ServiceHost
{
    public class Startup
    {
        #region CtorAndInjection
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        #endregion

        public void ConfigureServices(IServiceCollection services)
        {


            var connectionString = Configuration.GetConnectionString("LampShadeDb");


            #region ServicesManualBootstrapper
            ShopManagementBootstrapper.Configure(services, connectionString);
            DiscountManagementBootstrapper.Configure(services, connectionString);
            InventoryManagementBootstrapper.Configure(services,connectionString);
            BlogBootstrapper.Configure(services,connectionString);
            CommentManagementBootstrapper.ConfigureService(services,connectionString);
            AccountManagementBootstrapper.Configure(services,connectionString);
            #endregion

            #region ManualService
            services.AddTransient<IFileUploader,FileUploader>();
            services.AddSingleton<IPasswordHasher,PasswordHasher>();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
            services.AddTransient<IAuthHelper, AuthHelper>();
            #endregion

            #region OriginalService

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Account");
                    o.LogoutPath = new PathString("/Account");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            #endregion
        }


        #region MiddleWare
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseAuthentication();
             
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
        #endregion
    }
}
