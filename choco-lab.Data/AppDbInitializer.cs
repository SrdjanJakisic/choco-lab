using choco_lab.Data.Models;
using choco_lab.Data.Static;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace choco_lab.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Categories

                if(!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Органске"
                        },
                        new Category()
                        {
                            Name = "Неорганске"
                        }
                    });
                    context.SaveChanges();
                }

                //Chocolates
                if (!context.Chocolates.Any())
                {
                    context.Chocolates.AddRange(new List<Chocolate>()
                    {
                        new Chocolate()
                        {
                            Name ="FirstChocolate",
                            Image = "pic1.jpg",
                            Price = 100,
                            ShortDescription = "This is first chocolate",
                            DetailedDescription="Everithing about chocolate",
                            CategoryId = 1,
                            Weight=100,
                            ExpirationDate="Годину дана од куповине"
                        },
                        new Chocolate()
                        {
                            Name ="SecondChocolate",
                            Image = "pic2.jpg",
                            Price = 150,
                            ShortDescription = "This is second chocolate",
                            DetailedDescription="Everithing about chocolate",
                            CategoryId = 2,
                            Weight=100,
                            ExpirationDate="Годину дана од куповине",
                            Quantity=5
                        },
                        new Chocolate()
                        {
                            Name ="ThirdChocolate",
                            Image = "pic3.jpg",
                            Price = 50,
                            ShortDescription = "This is third chocolate",
                            DetailedDescription="Everithing about chocolate",
                            CategoryId = 1,
                            Weight=50,
                            ExpirationDate="Годину дана од куповине",
                            Quantity=3
                        },
                        new Chocolate()
                        {
                            Name ="FourthChocolate",
                            Image = "pic4.jfif",
                            Price = 50,
                            ShortDescription = "This is fourth chocolate",
                            DetailedDescription="Everithing about chocolate",
                            CategoryId = 1,
                            Weight=50,
                            ExpirationDate="Годину дана од куповине",
                            Quantity=4
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles section
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@chocolab.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "app-admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        City = "Панчево",
                        Address = "Admin Address",
                        PhoneNumber = "0652299666"
                        
                    };
                    await userManager.CreateAsync(newAdminUser, "admin@1234");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@chocolab.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (adminUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        City = "Београд",
                        Address = "User Address",
                        PhoneNumber = "0602268844"
                    };
                    await userManager.CreateAsync(newAppUser, "user@1234");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
