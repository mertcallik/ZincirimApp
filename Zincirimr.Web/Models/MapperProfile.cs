using AutoMapper;
using Zincirimr.Data.Models;
using Zincirimr.Web.Components;
using Zincirimr.Web.ViewModels;

namespace Zincirimr.Web.Models
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<AuthRegisterViewModel, AppUser>();
            CreateMap<AuthLoginViewModel, AppUser>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Product,ProductViewModel>();
        }
    }
}
