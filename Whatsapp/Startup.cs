using EmailService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.BusinessLogic.Repos;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Interface;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.ViewModel;
using Whatsapp.Services;

namespace Whatsapp
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

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("WhatsappContextConnection"));
            });
            //services.AddDefaultIdentity<IdentityUser>()
            // .AddRoles<IdentityRole>()
            // .AddEntityFrameworkStores<ApplicationContext>();


            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddScoped(typeof(IRepository<City>), typeof(Repository<City>));
            services.AddScoped(typeof(IRepository<Users>), typeof(Repository<Users>));
            services.AddScoped(typeof(IRepository<EmailSetting>), typeof(Repository<EmailSetting>));
            services.AddControllersWithViews();


            IEmailConfiguration emailConfig = new EmailConfiguration
            {
                SmtpServer = "smtp.gmail.com",
                SmtpPassword = "Hardware@2020",
                SmtpUsername = "support@roundpay.in",
                SmtpPort = 587,
            };
            services.AddScoped<IEmailService, EmailFactory>();
            services.AddScoped<IEmailConfiguration, EmailConfiguration>();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                //options.Cookie.Expiration 

                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
                //options.ReturnUrlParameter=""
            });
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
