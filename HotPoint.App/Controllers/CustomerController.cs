
using HotPoint.App.Utils.Constants;
using HotPoint.App.Utils.Helpers;
using HotPoint.Entities;
using HotPoint.Models.CommonModels;
using HotPoint.Models.InputModels;
using HotPoint.Services;
using HotPoint.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotPoint.App.Controllers
{
    [Authorize(Roles = RoleType.Customer)]
    public class CustomerController : Controller
    {
        private readonly CustomerService customerService;
        private readonly ShoppingService shoppingService;
        private readonly UserManager<AppUser> userManager;

        public CustomerController(CustomerService customerService, 
            ShoppingService shoppingService,
            UserManager<AppUser> userManager)
        {
            this.customerService = customerService;
            this.shoppingService = shoppingService;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Panel()
        {
            string cartKey = SessionKeys.Cart + this.userManager.GetUserId(this.User);

            var shoppingCart = this.HttpContext.Session.GetObjectFromJson<ShoppingCart>(cartKey);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();                
            }

            var model = this.customerService.GetCustomerPanelModel();

            model.ShoppingCart = shoppingCart;            

            this.HttpContext.Session.SetObjectAsJson(cartKey, shoppingCart);

            return View(model);
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            string cartKey = SessionKeys.Cart + this.userManager.GetUserId(this.User);

            var shoppingCart = this.HttpContext.Session.GetObjectFromJson<ShoppingCart>(cartKey);

            var model = this.shoppingService.Checkout(shoppingCart);

            return View(model);
        }
    }
}