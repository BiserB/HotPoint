using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Models.ViewModels
{
    public class ManageUsersViewModel
    {
        public List<UserDetailsViewModel> Customers { get; set; }
        
        public List<UserDetailsViewModel> Managers { get; set; }
    }
}
