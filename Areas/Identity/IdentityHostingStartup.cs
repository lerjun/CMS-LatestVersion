using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using AuthSystem.Areas.Identity.Data;
using AOPC.Controllers;
using AOPC.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthSystem.Data;
using AuthSystem.Services;

[assembly: HostingStartup(typeof(AuthSystem.Areas.Identity.IdentityHostingStartup))]
namespace AuthSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        //private IConfiguration _config;
        //public IdentityHostingStartup(IConfiguration config) 
        //{
        //    _config = config;
        //}
        public void Configure(IWebHostBuilder builder)
        {
            
            builder.ConfigureServices((context, services) => {

                services.AddDbContext<AuthDbContext>(options =>
                       options.UseSqlServer(
                           context.Configuration.GetConnectionString("DevConnection")));
                services.AddDefaultIdentity<ApplicationUser>(options =>
                {

                    bool isConfirm = context.Configuration.GetValue<string>("AppSettings:CONFIRMED_ACCOUNT") == "1";
                    Debug.WriteLine("isConfirm " + isConfirm);
                    options.SignIn.RequireConfirmedAccount = isConfirm;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;

                })
                    .AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();
                services.AddTransient<AuthDbContext>();

                //Add Class Services
                services.AddScoped<GlobalService>();

            });
        }
    }
}