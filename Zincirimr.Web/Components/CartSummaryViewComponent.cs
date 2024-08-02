using Microsoft.AspNetCore.Mvc;
using Zincirimr.Web.Models;

namespace Zincirimr.Web.Components
{
    public class CartSummaryViewComponent:ViewComponent
    {
        public CartSummaryViewComponent(Cart cartService)
        {
            Cart = cartService;
        }
        public Cart? Cart { get; set; }
        public IViewComponentResult Invoke()
        {
            var model = Cart;
            return View(model);
        }
    }
}
