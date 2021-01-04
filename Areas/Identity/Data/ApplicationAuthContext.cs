using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BPUIO_OneForEachOther.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BPUIO_OneForEachOther.Data
{
    public class ApplicationAuthContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationAuthContext(DbContextOptions<ApplicationAuthContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ApplicationUser>().ToTable("cv_users");

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
