using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Entities
{
    public class Order : SeededEntity
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }
        
        public AppUser Customer { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal AmountTotal { get; set; }

        public int StatusId { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderProduct> Products { get; set; }
    }
}
