using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotPoint.App.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotPoint.App.Controllers
{
    [Authorize(Roles = RoleType.Administrator)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}