
using System.Collections.Generic;

namespace HotPoint.Models.CommonModels
{
    public class ShoppingCart
    {
        public List<Item> Items { get; set; } = new List<Item>();

        public decimal TotalAmount { get; set; }
    }
}
