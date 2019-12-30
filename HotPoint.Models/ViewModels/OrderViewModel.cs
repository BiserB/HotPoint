using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal AmountTotal { get; set; }
        
        public string Status { get; set; }
    }
}
