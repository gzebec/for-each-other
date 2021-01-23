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
                    new Role{Name="Administrator",Description="Administrator role",Status="Active",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin"},
                    new Role{Name="Contributor",Description="Contributer role",Status="Active",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin"}
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
                    new AuthenticationScheme{Name="Standard authentication scheme",Status="Active",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin"},
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
                    new Country{Code="hr",Name="Croatia",Language="hr",Status="Active",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin"}
                };
                foreach (Country c in countries)
                {
                    context.Countries.Add(c);
                }
                context.SaveChanges();
            }

            if (!context.Cities.Any())
            {
                var cities = new City[]
                {
                    new City{Code="ZG",Name="Zagreb",Status="Active",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin",CountryId=1}
                };
                foreach (City c in cities)
                {
                    context.Cities.Add(c);
                }
                context.SaveChanges();
            }

            if (!context.Boroughs.Any())
            {
                var boroughs = new Borough[]
                {
                    new Borough{Code="ZGGD",Name="Gornja Dubrava",Status="Active",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin",CityId=1}
                };
                foreach (Borough c in boroughs)
                {
                    context.Boroughs.Add(c);
                }
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User{Username="admin",Password="A94A8FE5CCB19BA61C4C0873D391E987982FBBD3",FirstName="Admin",LastName="User",Email="gzebec@gmail.com",Lat="45.828148899999995",Lng="16.0746871",Status="Active",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin",CountryId=1, AuthenticationSchemeId=1}
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
                new UserRole{UserId=1,RoleId=1,Status="Active",Created=DateTime.Now,CreatedBy="Admin",Updated=DateTime.Now,UpdatedBy="Admin"}
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