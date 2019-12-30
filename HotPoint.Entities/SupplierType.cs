using System.Collections.Generic;

namespace HotPoint.Entities
{
    public class SupplierType : SeededEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public List<Supplier> Suppliers { get; set; }
    }
}