using BPUIO_OneForEachOther.Models;
using System;
using System.Linq;

namespace BPUIO_OneForEachOther.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            // Look for any roles.
            /*if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }*/


            if (!context.Roles.Any()) {
                var roles = new Role[]
                {
                    new Role{Description="Administrator role",Status="A",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin"},
                    new Role{Description="Contributer role",Status="A",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin"}
                };
                foreach (Role r in roles)
                {
                    context.Roles.Add(r);
                }
                context.SaveChanges();
            }

            if (!context.AuthenticationSchemes.Any())
            {
                var schemes = new AuthenticationScheme[]
                {
                    new AuthenticationScheme{Name="Standard authentication scheme",Status="A",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin"},
                };
                foreach (AuthenticationScheme r in schemes)
                {
                    context.AuthenticationSchemes.Add(r);
                }
                context.SaveChanges();
            }

            if (!context.Countries.Any())
            {
                var countries = new Country[]
                {
                    new Country{Code="hr",Name="Croatia",Language="hr",Status="A",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin"}
                };
                foreach (Country c in countries)
                {
                    context.Countries.Add(c);
                }
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User{Username="admin",Password="admin11",FirstName="Admin",LastName="User",Email="gzebec@gmail.com",Status="A",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin",CountryId=1, AuthenticationSchemeId=1}
                };
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();
            }

            if (!context.UserRoles.Any())
            {
                var userRoles = new UserRole[]
{
                new UserRole{UserId=1,RoleId=1,Status="A",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin"}
};
                foreach (UserRole u in userRoles)
                {
                    context.UserRoles.Add(u);
                }
                context.SaveChanges();
            }
        }
    }
}