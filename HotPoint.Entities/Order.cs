using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal AmountTotal { get; set; }

        public int StatusId { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
