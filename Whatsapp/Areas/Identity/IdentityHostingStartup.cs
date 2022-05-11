using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Whatsapp.Models.Data;

[assembly: HostingStartup(typeof(Whatsapp.Areas.Identity.IdentityHostingStartup))]
namespace Whatsapp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("WhatsappContextConnection")));

                //services.AddDefaultIdentity<WhatsappUser>(options => options.SignIn.RequireConfirmedAccount = false)
                //    .AddEntityFrameworkStores<ApplicationContext>().AddRoles<IdentityRole>();
                services.AddIdentity<WhatsappUser, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = false;
                })
    .AddEntityFrameworkStores<ApplicationContext>();

            });
        }
    }
}