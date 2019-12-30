
using System.ComponentModel.DataAnnotations.Schema;

namespace HotPoint.Entities
{
     public class OrderProduct : SeededEntity
    {
        [Column(Order = 1)]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Column(Order = 2)]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
