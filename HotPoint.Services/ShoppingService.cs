using HotPoint.Data;
using HotPoint.Entities;
using HotPoint.Models.CommonModels;
using HotPoint.Models.ViewModels;
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

        public Dictionary<ProductViewModel, int> Checkout(ShoppingCart shoppingCart)
        {
            var cartProducts = new Dictionary<ProductViewModel, int>();

            foreach (var item in shoppingCart.Items)
            {
                var currentProduct = this.db.Products.FirstOrDefault(p => p.Id == item.ProductId);

                if (currentProduct == null)
                {
                    throw new ArgumentException("Invalid shopping cart item");
                }

                var model = new ProductViewModel()
                {
                    Id = currentProduct.Id,
                    Name = currentProduct.Name,
                    PhotoName = currentProduct.PhotoName,
                    Description = currentProduct.Description,
                    Price = currentProduct.Price
                };

                cartProducts.Add(model, item.Quantity);
            }

            return cartProducts;
        }
    }
}
