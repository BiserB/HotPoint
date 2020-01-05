using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Entities
{
    public class Package
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Material { get; set; }

        public decimal Volume { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}


