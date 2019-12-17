using HotPoint.App.Utils.Constants;
using HotPoint.Data;
using HotPoint.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HotPoint.App.Utils
{
    public static class DbSeeder
    {
        public static async void SeedRoles(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var roles = new IdentityRole[]
                {
                    new IdentityRole(RoleType.Customer),
                    new IdentityRole(RoleType.Manager),
                    new IdentityRole(RoleType.Administrator),
                };

                foreach (var role in roles)
                {
                    var roleDoesNotExist = !await roleManager.RoleExistsAsync(role.Name);

                    if (roleDoesNotExist)
                    {
                        var creation = await roleManager.CreateAsync(role);

                        if (!creation.Succeeded)
                        {
                            throw new InvalidOperationException($"Creation of {role} role failed.");
                        }
                    }
                }
            }
        }

        public static async void SeedUsers(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();

                var admin = await userManager.FindByNameAsync(RoleType.Administrator);

                if (admin == null)
                {
                    admin = new AppUser()
                    {
                        UserName = "admin@hotpoint.bg",
                        Email = "admin@hotpoint.bg",
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(admin, "1111");

                    await userManager.AddToRoleAsync(admin, RoleType.Administrator);
                }
            }
        }

        public static void SeedInitalData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<HotPointDbContext>();

                SeedEntity<IngredientType>(db, Constants.App.IngredientTypesFilepath);
                SeedEntity<Ingredient>(db, Constants.App.IngredientsFilepath);

                SeedEntity<SupplierType>(db, Constants.App.SupplierTypeFilepath);
                SeedEntity<Supplier>(db, Constants.App.SuppliersFilepath);

                SeedEntity<Category>(db, Constants.App.CategoriesFilepath);
                SeedEntity<Recipe>(db, Constants.App.RecipesFilepath);

                SeedEntity<RecipeIngredient>(db, Constants.App.RecipeIngredientsFilepath);

                SeedEntity<Product>(db, Constants.App.ProductsFilepath);

                SeedEntity<OrderStatus>(db, Constants.App.OrderStatusesFilepath);
            }
        }        

        private static void SeedEntity<E>(HotPointDbContext db, string filePath) where E: SeededEntity
        {
            string execPath = Assembly.GetExecutingAssembly().Location;

            string basePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(execPath), Constants.App.PathCorrection));

            string fullPath = Path.Combine(basePath, filePath);

            var jsonData = File.ReadAllText(fullPath);

            var entityData = JsonConvert.DeserializeObject<E[]>(jsonData);

            var dbSet = db.Set<E>();

            if (!dbSet.Any())
            {
                dbSet.AddRange(entityData);
            }           

            db.SaveChanges();
        }
    }
}
