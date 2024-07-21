namespace Zincirimr.Web.ViewModels
{
    public class ProductViewModel
    {
        public string? Name { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty;
        public double? Price { get; set; }
        public string? Image { get; set; } = string.Empty;
    }
    public class ProductListViewModel
    {
        public List<ProductViewModel> ProductsList { get; set; } = new List<ProductViewModel>();
        public PageInfo PageInfo { get; set; }
    }
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

    }
}
