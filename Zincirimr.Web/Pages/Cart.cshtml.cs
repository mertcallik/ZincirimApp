using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zincirimr.Data.Abstract;
using Zincirimr.Web.Helpers;
using Zincirimr.Web.Models;

namespace Zincirimr.Web.Pages
{
    public class CartModel : PageModel
    {
        private IProductRepository _productRepository;

        public CartModel(IProductRepository productRepository )
        {
            _productRepository = productRepository;
        }

        public Cart? Cart { get; set; }
        public void OnGet()
        {
          Cart=HttpContext.Session.GetJson<Cart>("cart");

        }
        public IActionResult OnPost(string? url)
        {
            if (!string.IsNullOrEmpty(url))
            {

                var product = _productRepository.Products.FirstOrDefault(p => p.Url == url);
                if (product!=null)
                {
                    Cart = HttpContext.Session.GetJson<Cart>("cart")??new Cart();
                   Cart.AddItem(product, 1);
                   HttpContext.Session.SetJson("cart",Cart);
                
                }

            }

            
            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostRemove(string? url)
        {
            if (!string.IsNullOrEmpty(url))
            {

                var product = _productRepository.Products.FirstOrDefault(p => p.Url == url);
                if (product != null)
                {
                    Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                    Cart.Remove(product);
                    HttpContext.Session.SetJson("cart", Cart);

                }

            }


            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostClear()
        {
            Cart=HttpContext.Session.GetJson<Cart>("cart")??new Cart();
            Cart.RemoveAll();
            HttpContext.Session.SetJson("cart", Cart);
            return RedirectToPage("/Cart");
        }
    }
}
