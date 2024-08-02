using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zincirimr.Data.Abstract;
using Zincirimr.Data.Models;
using Zincirimr.Web.Models;
using Zincirimr.Web.ViewModels;

namespace Zincirimr.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly Cart cart;
        private readonly IAuthRepository _authRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderController(Cart cartService, IAuthRepository authRepository,IAddressRepository addressRepository, IOrderRepository orderRepository)
        {
            cart = cartService;
            _authRepository = authRepository;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> CheckOut()
        {
            var user = await _authRepository.FindUserFromId(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = new OrderViewModel()
            {
                Cart = cart,
                AppUser = user,
                Address = _addressRepository.ShowAddressesByUserId(user.Id).FirstOrDefault(c=>c.IsActive)??new Address(),
                

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(OrderViewModel model)
        {
            if (cart.CartItems.Count==0)
            {
                ModelState.AddModelError("", "Sepetiniz Boş");
                return View(model);
            }
            if (!ModelState.IsValid)
            {
                model.Cart = cart;
                ModelState.AddModelError("", "Tüm alanların doldurulması gereklidir");
                return View(model);
            }
            model.AppUser = await _authRepository.FindUserFromId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            model.Address = _addressRepository.ShowAddressesByUserId(model.AppUser.Id).FirstOrDefault(c => c.IsActive) ?? new Address();

            var order = new Order()
            {
                AddressId = model.Address.Id,
                Id = model.Id,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "",
                OrderItems = cart.CartItems.Select(c=>new OrderItem()
                {
                    ProductId = c.Product.Id,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                }).ToList()

            };
            model.Cart=cart;
            await _orderRepository.SaveOrder(order);
            return RedirectToAction("Index", "Home");
        }
    }
}
