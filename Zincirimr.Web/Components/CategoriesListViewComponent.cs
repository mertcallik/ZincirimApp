using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zincirimr.Data.Abstract;
using Zincirimr.Web.ViewModels;

namespace Zincirimr.Web.Components
{
    public class CategoriesListViewComponent:ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesListViewComponent(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var category = _categoryRepository.Categories.Distinct();
            var categoryToMap = _mapper.Map<IEnumerable<CategoryViewModel>>(category);
            var model = new CategoryListViewModel()
            {
                CategoryListViewModels = categoryToMap
            };

            return View(model);

        }


    }
}
