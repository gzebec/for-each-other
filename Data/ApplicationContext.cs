using BPUIO_OneForEachOther.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BPUIO_OneForEachOther.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<AuthenticationScheme> AuthenticationSchemes { get; set; }
        public DbSet<Borough> Boroughs { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBorough> UserBoroughs { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthenticationScheme>().ToTable("cv_authentication_schemes");
            modelBuilder.Entity<Country>().ToTable("cv_countries");
            modelBuilder.Entity<City>().ToTable("cv_cities");
            modelBuilder.Entity<Borough>().ToTable("cv_boroughs");
            modelBuilder.Entity<User>().ToTable("cv_users");
            modelBuilder.Entity<Role>().ToTable("cv_roles");
            modelBuilder.Entity<Order>().ToTable("cv_orders");
            modelBuilder.Entity<OrderDetail>().ToTable("cv_order_details");
            modelBuilder.Entity<UserBorough>().ToTable("cv_user_boroughs");
            modelBuilder.Entity<UserNotification>().ToTable("cv_user_notifications");
            modelBuilder.Entity<UserRole>().ToTable("cv_user_roles");

        }
    }
}