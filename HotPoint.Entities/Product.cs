using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }
    }
}
