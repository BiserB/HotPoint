using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotPoint.Services;
using HotPoint.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotPoint.App.Controllers
{
    [Authorize(Roles = RoleType.Customer)]
    public class CustomerController : Controller
    {
        private readonly CustomerService service;

        public CustomerController(CustomerService service)
        {
            this.service = service;
        }

        public IActionResult Selection()
        {
            var model = this.service.GetCustomerSelectionModel();

            return View(model);
        }
    }
}