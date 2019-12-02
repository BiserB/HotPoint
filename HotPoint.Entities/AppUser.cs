using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HotPoint.Entities
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedOn { get; set; }

        public DateTime LoggedOn { get; set; }

        public string Organization { get; set; }

        public string Language { get; set; }
        
        public bool AnalyticalCookiesAllowed { get; set; }

        public bool NewsletterSubscription { get; set; }

        public List<Order> Orders { get; set; }
    }
}
