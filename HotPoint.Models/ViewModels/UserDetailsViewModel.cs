using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Models.ViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LoggedOn { get; set; }

        public string Organization { get; set; }
    }
}
