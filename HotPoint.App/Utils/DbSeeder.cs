using HotPoint.Data;
using HotPoint.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
                SeedIngredientTypes(db);
                SeedIngredients(db);
                SeedRecipes(db);
            }
        }

        private static void SeedSuppliers(HotPointDbContext db)
        {
            var internalSupplier = db.SupplierTypes.FirstOrDefault(st => st.Name == Supplier.Internal);

            if (internalSupplier == null)
            {
                db.SupplierTypes.Add(
                    new SupplierType()
                    {
                        Name = Supplier.Internal
                    });
            }

            var externalSupplier = db.SupplierTypes.FirstOrDefault(st => st.Name == Supplier.External);

            if (externalSupplier == null)
            {
                db.SupplierTypes.Add(
                    new SupplierType()
                    {
                        Name = Supplier.External
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

        private static void SeedIngredientTypes(HotPointDbContext db)
        {
            string[] types = new string[]
            {
                "hypoallergenic",
                "allergenic"
            };

            foreach (var type in types)
            {
                if (!db.IngredientTypes.Any(t => t.Description == type))
                {
                    db.IngredientTypes.Add(new IngredientType()
                    {
                        Description = type
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
                        IngredientType = 1
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
        
    }
}
