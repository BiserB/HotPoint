using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotPoint.App.Utils;
using HotPoint.Models.InputModels;
using HotPoint.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotPoint.App.Controllers
{
    [Authorize(Roles = RoleType.Administrator)]
    public class AdminController : Controller
    {
        private AdminService service;

        public AdminController(AdminService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var model = this.service.GetDashboardModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeUserRole(UserInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.service.ChangeUserRole();

            return View();
        }
    }
}