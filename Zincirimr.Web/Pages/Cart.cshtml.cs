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

        public CartModel(IProductRepository productRepository, Cart cartService)
        {
            _productRepository = productRepository;
            Cart = cartService;
        }

        public Cart? Cart { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost(string? url)
        {
            if (!string.IsNullOrEmpty(url))
            {

                var product = _productRepository.Products.FirstOrDefault(p => p.Url == url);
                if (product!=null)
                {
                    Cart?.AddItem(product, 1);
                
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
                    Cart?.Remove(product);

                }

            }


            return RedirectToPage("/Cart");
        }

        public IActionResult OnPostClear()
        {
            Cart?.RemoveAll();
            return RedirectToPage("/Cart");
        }
    }
}
