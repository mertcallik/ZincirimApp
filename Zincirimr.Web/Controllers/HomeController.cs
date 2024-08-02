using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Zincirimr.Data.Abstract;
using Zincirimr.Data.Models;
using Zincirimr.Web.Models;
using Zincirimr.Web.Components;
using Zincirimr.Web.ViewModels;

namespace Zincirimr.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, IMapper mapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        private int _pageSize = 11;

        [HttpGet] 
        public IActionResult Index(string? category, int page = 1)
        {

            var model = new ProductListViewModel()
            {
                ProductsList = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(_productRepository.GetProductsByCategory(category??"", page, _pageSize)).ToList(),
                PageInfo = new PageInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _productRepository.GetProductCount(category)
                }
            };
            
            return View(model);
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
