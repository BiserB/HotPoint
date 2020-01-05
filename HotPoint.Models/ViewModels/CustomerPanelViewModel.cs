
using HotPoint.Models.CommonModels;
using System.Collections.Generic;

namespace HotPoint.Models.ViewModels
{
    public class CustomerPanelViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

        public ShoppingCart ShoppingCart { get; set; }
    }
}
