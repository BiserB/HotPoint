using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotPoint.Entities;
using HotPoint.Data;
using HotPoint.Models.ViewModels;

namespace HotPoint.App.Controllers
{
    public class HomeController : Controller
    {
        private HotPointDbContext db;

        public HomeController(HotPointDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            Recipe recipe = new Recipe()
            {
                Directions = "Index created recipe",
                Notes = "Demo recipe"
            };

            db.Recipes.Add(recipe);

            db.SaveChanges();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
