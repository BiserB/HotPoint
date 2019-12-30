﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Entities
{
    public class Category : SeededEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
