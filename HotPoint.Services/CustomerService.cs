using HotPoint.Data;
using HotPoint.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotPoint.Services
{
    public class CustomerService
    {
        private readonly HotPointDbContext db;

        public CustomerService(HotPointDbContext db)
        {
            this.db = db;
        }

        public CustomerSelectionViewModel GetCustomerSelectionModel()
        {
            var model = new CustomerSelectionViewModel();

            model.Foods = this.db.Products.Select(p => p.Name).ToList();

            return model;
        }
    }
}
