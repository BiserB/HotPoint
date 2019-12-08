using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int UsersCount { get; set; }

        public int FoodCategoriesCount { get; set; }

        public int OrdersCount { get; set; }

        public int RecipesCount { get; set; }

        public int IngredientsCount { get; set; }

        public int ProductsCount { get; set; }

        public int InternalSuppliers { get; set; }

        public int ExternalSuppliers { get; set; }
    }
}
