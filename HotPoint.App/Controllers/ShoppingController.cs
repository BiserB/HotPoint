using HotPoint.App.Utils.Constants;
using HotPoint.App.Utils.Helpers;
using HotPoint.Entities;
using HotPoint.Models.CommonModels;
using HotPoint.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotPoint.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ShoppingService shoppingService;
        private readonly UserManager<AppUser> userManager;

        public ShoppingController(ShoppingService shoppingService, UserManager<AppUser> userManager)
        {
            this.shoppingService = shoppingService;
            this.userManager = userManager;            
        }

        [HttpGet]
        [ActionName("getCart")]
        public ActionResult<ShoppingCart> GetCart()
        {
            var sessionId = this.HttpContext.Session.Id;

            var cartKey = SessionKeys.Cart + this.userManager.GetUserId(this.User);

            var cart = this.HttpContext.Session.GetObjectFromJson<ShoppingCart>(cartKey);

            return cart;
        }

        [HttpPost]
        [ActionName("addToCart")]
        public ActionResult<ShoppingCart> AddToCart([FromBody]Item item)
        {
            var cartKey = SessionKeys.Cart + this.userManager.GetUserId(this.User);

            var shoppingCart = this.HttpContext.Session.GetObjectFromJson<ShoppingCart>(cartKey);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
            }

            this.shoppingService.AddToCart(item, shoppingCart);

            this.HttpContext.Session.SetObjectAsJson(cartKey, shoppingCart);

            return shoppingCart;
        }

        [HttpPost]
        [ActionName("removeFromCart")]
        public ActionResult<ShoppingCart> RemoveFromCart([FromBody]Item item)
        {
            var cartKey = SessionKeys.Cart + this.userManager.GetUserId(this.User);

            var shoppingCart = this.HttpContext.Session.GetObjectFromJson<ShoppingCart>(cartKey);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart();
                return shoppingCart;
            }

            this.shoppingService.RemoveFromCart(item, shoppingCart);

            this.HttpContext.Session.SetObjectAsJson(cartKey, shoppingCart);

            return shoppingCart;
        }
    }
}