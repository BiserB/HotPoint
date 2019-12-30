using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Models.ViewModels
{
    public class ManagerPanelViewModel
    {
        public string Date { get; set; }

        public int CustomersCount { get; set; }

        public int CountOfOrders { get; set; }

        public List<OrderViewModel> NewOrders { get; set; }

        public List<OrderViewModel> ProcessedOrders { get; set; }

        public List<OrderViewModel> CompletedOrders { get; set; }
        
        public int FoodCategoriesCount { get; set; }

        public int RecipesCount { get; set; }

        public int IngredientsCount { get; set; }

        public int ProductsCount { get; set; }

        public int InternalSuppliers { get; set; }

        public int ExternalSuppliers { get; set; }
    }
}
