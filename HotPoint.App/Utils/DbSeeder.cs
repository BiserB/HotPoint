
using HotPoint.Data;
using HotPoint.Entities;
using HotPoint.Shared;
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
        public static async void SeedAppData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                await SeedRoles(serviceScope);

                await SeedAdmin(serviceScope);

                SeedInitalData(app);
            }
        }

        private static async Task SeedRoles(IServiceScope serviceScope)
        {
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            var roles = typeof(RoleType)
                        .GetFields()
                        .Select(f => new IdentityRole(f.GetValue(null).ToString()));

            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role.Name);

                if (!roleExists)
                {
                    var creation = await roleManager.CreateAsync(role);

                    if (!creation.Succeeded)
                    {
                        throw new InvalidOperationException($"Creation of {role} role failed.");
                    }
                }
            }
        }

        private static async Task SeedAdmin(IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();

            string adminEmail = "admin@hotpoint.bg";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new AppUser()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    CreatedOn = DateTime.Now,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "1111");

                var roles = typeof(RoleType).GetFields().Select(f => f.GetValue(null).ToString());

                await userManager.AddToRolesAsync(admin, roles);
            }
        }

        private static void SeedInitalData(IApplicationBuilder app)
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

                SeedEntity<ProductStatus>(db, Constants.App.ProductStatusesFilepath);
                SeedEntity<Product>(db, Constants.App.ProductsFilepath);

                SeedOrderStatuses(db);
                //SeedEntity<OrderStatus>(db, Constants.App.OrderStatusesFilepath);
            }
        }

        private static void SeedEntity<E>(HotPointDbContext db, string filePath) where E : SeededEntity
        {
            var dbSet = db.Set<E>();

            if (dbSet.Any())
            {
                return;
            }

            string execPath = Assembly.GetExecutingAssembly().Location;

            string basePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(execPath), Constants.App.PathCorrection));

            string fullPath = Path.Combine(basePath, filePath);

            var jsonData = File.ReadAllText(fullPath);

            var entityData = JsonConvert.DeserializeObject<E[]>(jsonData);

            dbSet.AddRange(entityData);

            db.SaveChanges();
        }

        private static void SeedOrderStatuses(HotPointDbContext db)
        {
            var statuses = Shared.OrderStatus.GetAll<Shared.OrderStatus>().OrderBy(s => s.Id);

            foreach (var status in statuses)
            {
                if (!db.OrderStatuses.Any(os => os.Id == status.Id))
                {
                    var newOrderStatus = new Entities.OrderStatus()
                    {
                        Id = status.Id,
                        Description = status.Name
                    };

                    db.OrderStatuses.Add(newOrderStatus);
                }
            }

            db.SaveChanges();
        }

    }
}
