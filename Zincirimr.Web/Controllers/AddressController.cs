using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Zincirimr.Data.Abstract;
using Zincirimr.Data.Models;
using Zincirimr.Web.ViewModels;

namespace Zincirimr.Web.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _adressRepository;
        private IMapper _mapper;


        public AddressController(IAddressRepository adressRepository, IMapper mapper)
        {
            _adressRepository = adressRepository;
            _mapper = mapper;
        }


        public IActionResult ShowAddresses()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = new AddressViewModel()
            {
                AdressList = new AddressListViewModel
                {
                    Addressess = _mapper
                        .Map<IEnumerable<AddressViewModel>>(_adressRepository.ShowAddressesByUserId(id ?? null))
                        .ToList()
                }
            };
            return View(model);
        }
        

        [HttpPost]
        public async Task<IActionResult> AddAddress(AddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["confirmMessage"] = "Lütfen tüm alanları eksiksiz doldurunuz.";
                return RedirectToAction("ShowAddresses");
            }
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "1";
            var adress = _mapper.Map<Address>(model);
            await _adressRepository.AddAddress(adress, id);
            return RedirectToAction("ShowAddresses");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAddress(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("ShowAddresses");
            }
            var message = await _adressRepository.DeleteAddress(id);
            TempData["confirmMessage"] = message;
            return RedirectToAction("ShowAddresses");
        }

        [HttpPost]
        public async Task<IActionResult> ActiveAddress(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)??"0";
            await _adressRepository.ActivateAdress(id, userId);
            return RedirectToAction("ShowAddresses");
        }

    }
}
