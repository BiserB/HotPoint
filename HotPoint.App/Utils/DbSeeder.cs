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

                SeedSuppliers(db);
                SeedFoodCategories(db);
                SeedEntity<IngredientType>(db, Constants.App.IngredientTypesFilepath);
                SeedEntity<Ingredient>(db, Constants.App.IngredientsFilepath);
                SeedRecipes(db);
            }
        }

        private static void SeedSuppliers(HotPointDbContext db)
        {
            var internalSupplier = db.SupplierTypes.FirstOrDefault(st => st.Name == Constants.SupplierType.Internal);

            if (internalSupplier == null)
            {
                db.SupplierTypes.Add(
                    new Entities.SupplierType()
                    {
                        Name = Constants.SupplierType.Internal
                    });
            }

            var externalSupplier = db.SupplierTypes.FirstOrDefault(st => st.Name == Constants.SupplierType.External);

            if (externalSupplier == null)
            {
                db.SupplierTypes.Add(
                    new Entities.SupplierType()
                    {
                        Name = Constants.SupplierType.External
                    });
            }

            db.SaveChanges();
        }

        private static void SeedFoodCategories(HotPointDbContext db)
        {
            string[] categoryNames = new string[]
            {
                "Bread",
                "Breakfast",
                "Pasta",
                "Pizza",
                "Soups",
                "Salads",
                "Vegetables",
                "Rice & Beans",
                "Seafood",
                "Meats",
                "Sandwiches",
                "Deserts",
            };

            foreach (var category in categoryNames)
            {
                if (!db.FoodCategories.Any(fc => fc.Name == category))
                {
                    db.FoodCategories.Add(new Category()
                    {
                        Name = category
                    });
                }
            }

            db.SaveChanges();
        }


        private static void SeedIngredients(HotPointDbContext db)
        {
            string[] ingredients = new string[]
            {
                "spaghetti",
                "salt",
                "egg",
                "bacon",
                "garlic"
            };

            foreach (var ingredient in ingredients)
            {
                if (!db.Ingredients.Any(i => i.Name == ingredient))
                {
                    db.Ingredients.Add(new Ingredient()
                    {
                        Name = ingredient,
                        TypeId = 1
                    });
                }
            }

            db.SaveChanges();
        }

        private static void SeedRecipes(HotPointDbContext db)
        {
            string[] recipes = new string[]
            {
                "Spaghetti Carbonara"
            };

            foreach (var recipeName in recipes)
            {
                if (!db.Recipes.Any(r => r.Name == recipeName))
                {
                    var recipe = new Recipe()
                    {
                        Name = recipeName,
                        Directions = ""
                    };

                    db.Recipes.Add(recipe);

                    foreach (var ingredient in db.Ingredients)
                    {
                        db.RecipeIngredient.Add(new RecipeIngredient()
                        {
                            RecipeId = recipe.Id,
                            IngredientId = ingredient.Id
                        });
                    }
                }
            }

            db.SaveChanges();
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

    public class Demo
    {

    }
}
