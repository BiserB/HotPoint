using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotPoint.Models.InputModels
{
    public class UserInputModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserRoleId { get; set; }
    }
}
