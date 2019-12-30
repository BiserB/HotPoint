using HotPoint.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HotPoint.Models.ViewModels;
using HotPoint.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using HotPoint.Shared;
using System.Security.Claims;

namespace HotPoint.Services
{
    public class AdminService
    {
        private HotPointDbContext db;
        private UserManager<AppUser> userManager;

        public AdminService(HotPointDbContext db, UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            this.db = db;
        }

        public AdminPanelViewModel GetPanelModel()
        {
            var model = new AdminPanelViewModel();

            model.UsersCount = this.userManager.Users.Count();
            model.ProductsCount = this.db.Products.Count();
            model.OrdersCount = this.db.Orders.Count();
            model.RecipesCount = this.db.Recipes.Count();
            model.FoodCategoriesCount = this.db.FoodCategories.Count();
            model.IngredientsCount = this.db.Ingredients.Count();
            model.InternalSuppliers = this.db.Suppliers.Where(s => s.TypeId == 1).Count();
            model.ExternalSuppliers = this.db.Suppliers.Where(s => s.TypeId == 2).Count();

            return model;
        }

        public List<UserViewModel> FindUsers(string username)
        {
            var users = this.db.AppUsers
                .Where(u => u.UserName.Contains(username))
                .Select(u => new UserViewModel()
                {
                    UserId = u.Id,
                    Username = u.UserName
                })
                .ToList();

            return users;
        }

        public List<UserViewModel> GetLast10Users()
        {
            var users = this.db.AppUsers
                .OrderByDescending(u => u.CreatedOn)
                .Take(10)
                .Select(u => new UserViewModel()
                {
                    UserId = u.Id,
                    Username = u.UserName
                })
                .ToList();

            return users;
        }

        public List<UserViewModel> GetLastTenUsers()
        {
            var users = this.userManager.Users
                .OrderByDescending(u => u.CreatedOn)
                .Take(10)
                 .Select(u => new UserViewModel()
                 {
                     UserId = u.Id,
                     Username = u.UserName
                 })
                .ToList();

            return users;
        }

        public async Task<ManageUsersViewModel> GetUsers()
        {
            var model = new ManageUsersViewModel();

            model.Customers = (await this.userManager.GetUsersInRoleAsync(RoleType.Customer))
                .Select(u =>
                new UserDetailsViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    CreatedOn = u.CreatedOn,
                    LoggedOn = u.LoggedOn,
                    Organization = u.Organization,
                })
                .ToList();

            model.Managers = (await this.userManager.GetUsersInRoleAsync(RoleType.Manager))
                .Select(u =>
                new UserDetailsViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    CreatedOn = u.CreatedOn,
                    LoggedOn = u.LoggedOn,
                    Organization = u.Organization,
                })
                .ToList();
            
            return model;
        }


        public async Task<bool> AddToRole(string userId, string role)
        {
            var currentUser = this.db.AppUsers.FirstOrDefault(u => u.Id == userId);

            if(currentUser == null)
            {
                return false;
            }

            var result = await this.userManager.AddToRoleAsync(currentUser, role);

            return result.Succeeded;
        }

        public async Task<bool> RemoveFromRole(string userId, string role)
        {
            var currentUser = this.db.AppUsers.FirstOrDefault(u => u.Id == userId);

            if (currentUser == null)
            {
                return false;
            }

            var result = await this.userManager.RemoveFromRoleAsync(currentUser, role);

            return result.Succeeded;
        }
    }
}
