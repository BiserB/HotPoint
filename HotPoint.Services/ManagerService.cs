using HotPoint.Data;
using HotPoint.Entities;
using HotPoint.Models.ViewModels;
using HotPoint.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotPoint.Services
{
    public class ManagerService
    {
        private readonly HotPointDbContext db;
        private readonly UserManager<AppUser> userManager;

        public ManagerService(HotPointDbContext db, UserManager<AppUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<ManagerPanelViewModel> GetManagerPanelModel()
        {
            var model = new ManagerPanelViewModel();

            var today = DateTime.Today;

            model.Date = today.ToShortDateString();

            model.CustomersCount = (await userManager.GetUsersInRoleAsync(RoleType.Customer)).Count;

            model.ProductsCount = db.Products.Count();

            model.InternalSuppliers = db.Suppliers.Where(s => s.TypeId == 1).Count();

            model.ExternalSuppliers = db.Suppliers.Where(s => s.TypeId == 2).Count();

            var todayOrders = db.Orders.Where(o => o.Timestamp.Date == today).ToList();

            model.CountOfOrders = todayOrders.Count;

            int newOrderStatusId = Shared.OrderStatus.New.Id;
            int processedOrderStatusId = Shared.OrderStatus.Processed.Id;
            int completedOrderStatusId = Shared.OrderStatus.Completed.Id;

            model.NewOrders = todayOrders.Where(o => o.StatusId == newOrderStatusId)
                                        .Select(o => MapToOrderViewModel(o))
                                        .ToList();

            model.ProcessedOrders = todayOrders.Where(o => o.StatusId == processedOrderStatusId)
                                            .Select(o => MapToOrderViewModel(o))
                                            .ToList();

            model.CompletedOrders = todayOrders.Where(o => o.StatusId == completedOrderStatusId)
                                            .Select(o => MapToOrderViewModel(o))
                                            .ToList();

            return model;
        }

        private OrderViewModel MapToOrderViewModel(Order o)
        {
            return new OrderViewModel()
            {
                OrderId = o.Id,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.NormalizedEmail
            };
        }
    }
}
