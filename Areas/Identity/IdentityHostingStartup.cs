using System;
using BPUIO_OneForEachOther.Areas.Identity.Data;
using BPUIO_OneForEachOther.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BPUIO_OneForEachOther.Areas.Identity.IdentityHostingStartup))]
namespace BPUIO_OneForEachOther.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationAuthContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ApplicationAuthContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<ApplicationAuthContext>();

            });
        }
    }
}