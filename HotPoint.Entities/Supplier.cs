using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Entities
{
    public class Supplier : SeededEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public SupplierType Type { get; set; }

        public List<Product> Products { get; set; }

        public bool IsDeleted { get; set; }
    }
}
