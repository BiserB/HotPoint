﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhotoName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }        
    }
}
