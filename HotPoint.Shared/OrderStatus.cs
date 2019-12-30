using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Shared
{
    public class OrderStatus : Enumeration
    {
        public static readonly OrderStatus New = new OrderStatus(1, "Нова");
        public static readonly OrderStatus Cancelled = new OrderStatus(2, "Отказана");
        public static readonly OrderStatus Processed = new OrderStatus(3, "Обработена");
        public static readonly OrderStatus Completed = new OrderStatus(5, "Завършена");

        public OrderStatus(int id, string name) : base(id, name)
        {
        }
    }
}
