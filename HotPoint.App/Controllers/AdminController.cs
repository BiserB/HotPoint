using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotPoint.Models.InputModels;
using HotPoint.Services;
using HotPoint.Shared;
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
        public IActionResult Panel()
        {
            var model = this.service.GetPanelModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddManagerRole(UserInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }                      

            bool success = await this.service.AddToRole(model.UserId, RoleType.Manager);

            if (!success)
            {
                return new BadRequestResult();
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromManagerRole(UserInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            bool success = await this.service.RemoveFromRole(model.UserId, RoleType.Manager);

            if (!success)
            {
                return new BadRequestResult();
            }

            return RedirectToAction("ManageUsers");
        }

        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            var model = await this.service.GetUsers();
            
            return View(model);
        }

    }
}