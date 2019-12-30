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
    [Authorize(Roles = RoleType.Manager)]
    public class ManagerController : Controller
    {
        private readonly ManagerService service;

        public ManagerController(ManagerService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Panel()
        {
            var model = await this.service.GetManagerPanelModel();

            return View(model);
        }
    }
}