using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<Order> Orders { get; set; }
    }
}
