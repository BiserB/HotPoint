using HotPoint.Data;
using HotPoint.Models.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotPoint.Services
{
    public class ShoppingService
    {
        private readonly HotPointDbContext db;

        public ShoppingService(HotPointDbContext db)
        {
            this.db = db;
        }

        public void AddToCart(Item item, ShoppingCart shoppingCart)
        {
            var product = this.db.Products.FirstOrDefault(p => p.Id == item.ProductId);

            if (product == null)
            {
                throw new ArgumentException("Invalid product Id");
            }

            var currentItem = shoppingCart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (currentItem == null)
            {
                currentItem = new Item();
                currentItem.ProductId = product.Id;
                currentItem.ProductName = product.Name;
                currentItem.Quantity = 0;
                shoppingCart.Items.Add(currentItem);
            }

            currentItem.Quantity += 1;

        }

        public void RemoveFromCart(Item item, ShoppingCart shoppingCart)
        {
            var product = this.db.Products.FirstOrDefault(p => p.Id == item.ProductId);

            if (product == null)
            {
                throw new ArgumentException("Invalid product Id");
            }

            var currentItem = shoppingCart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (currentItem == null)
            {
                return;
            }

            currentItem.Quantity -= 1;

            if (currentItem.Quantity <= 0)
            {
                shoppingCart.Items.Remove(currentItem);
            }

        }
    }
}
