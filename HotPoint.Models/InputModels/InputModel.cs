using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotPoint.Models.InputModels
{
    public class ProductInputModel
    {
        [Required]
        public int ProductId { get; set; }
    }
}
