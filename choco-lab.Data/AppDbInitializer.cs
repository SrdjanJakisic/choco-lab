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
                            Name ="Чоколада лешник",
                            Image = "img1.jpg",
                            Price = 4,
                            ShortDescription = "Чоколада са лешником",
                            DetailedDescription="Чоколада са Бадемовим млеком и лешником са 60% какаа",
                            CategoryId = 1,
                            Weight=80,
                            ExpirationDate="Годину дана од куповине",
                            Quantity=5
                        },
                        new Chocolate()
                        {
                            Name ="Чоколада малина",
                            Image = "img2.jpg",
                            Price = 4.5,
                            ShortDescription = "Тамна чоколада са Малином",
                            DetailedDescription="Тамна чоколада са сушеном Малином и 80% какаа. Идеална за особе које воле јаке тамне чоколаде",
                            CategoryId = 1,
                            Weight=80,
                            ExpirationDate="Годину дана од куповине",
                            Quantity=5
                        },
                        new Chocolate()
                        {
                            Name ="Чоколада Цимет-Јабука",
                            Image = "img3.jpg",
                            Price = 2.4,
                            ShortDescription = "Чоколада са циметом и јабуком",
                            DetailedDescription="Чоколада са циметом и сушеном јабуком са 70% какаа",
                            CategoryId = 2,
                            Weight=80,
                            ExpirationDate="Годину дана од куповине",
                            Quantity=3
                        },
                        new Chocolate()
                        {
                            Name ="Чоколада кикирики",
                            Image = "img4.jpg",
                            Price = 2.3,
                            ShortDescription = "Чоколада са Кикирикијем",
                            DetailedDescription="Чоколада са Кикирикијем и са 60% какаа. Идеална за особе које воле кремастије чоколаде",
                            CategoryId = 2,
                            Weight=80,
                            ExpirationDate="Годину дана од куповине",
                            Quantity=4
                        },
                        new Chocolate()
                        {
                            Name ="Чоколада брусница",
                            Image = "img5.jpg",
                            Price = 4,
                            ShortDescription = "Тамна чоколада са брусницомс",
                            DetailedDescription="Тамна чоколада са сушеном Брусницом са 80% какаа. Одличан избор за особе које воле тамне јаке чоколаде.",
                            CategoryId = 1,
                            Weight=80,
                            ExpirationDate="Годину дана од куповине",
                            Quantity=5
                        },
                        
                        new Chocolate()
                        {
                            Name ="Чоколада цимет-вишња",
                            Image = "img6.jpg",
                            Price = 2.4,
                            ShortDescription = "Тамна чоколада са циметом и вишњом",
                            DetailedDescription="Савршен спој цимета и вишње у чоколади са 70% какаа",
                            CategoryId = 2,
                            Weight=80,
                            ExpirationDate="Годину дана од куповине",
                            Quantity=5
                        },
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
