using HotPoint.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HotPoint.Models.ViewModels;
using HotPoint.Entities;
using Microsoft.AspNetCore.Identity;

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

        public DashboardViewModel GetDashboardModel()
        {
            var model = new DashboardViewModel();

            model.UsersCount = this.userManager.Users.Count();

            model.ProductsCount = this.db.Products.Count();

            model.OrdersCount = this.db.Orders.Count();

            model.InternalSuppliers = this.db.Suppliers.Where(s => s.TypeId == 1).Count();

            model.ExternalSuppliers = this.db.Suppliers.Where(s => s.TypeId == 2).Count();

            return model;
    }

        public List<UserViewModel> FindUsers(string username)
        {
            var users = this.db.Users
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
            var users = this.db.Users
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

        public bool ChangeUserRole()
        {
            return false;
        }
    }
}
