using HotPoint.Data;
using HotPoint.Models.ViewModels;
using System.Linq;

namespace HotPoint.Services
{
    public class CustomerService
    {
        private readonly HotPointDbContext db;

        public CustomerService(HotPointDbContext db)
        {
            this.db = db;
        }

        public CustomerPanelViewModel GetCustomerPanelModel()
        {
            var model = new CustomerPanelViewModel();

            model.Products = this.db.Products.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    PhotoName = p.PhotoName,
                    Description = p.Description,
                    Price = p.Price
                })
                .ToList();

            return model;
        }
    }
}
