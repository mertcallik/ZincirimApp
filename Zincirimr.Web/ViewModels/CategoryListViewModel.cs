namespace Zincirimr.Web.ViewModels
{
    public class CategoryListViewModel
    {
        public IEnumerable<CategoryViewModel> CategoryListViewModels { get; set; } = Enumerable.Empty<CategoryViewModel>();
    }

    public class CategoryViewModel
    {
        public string? Name { get; set; } = string.Empty;
        public string? Url { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public string? bootstrapicon { get; set; } = string.Empty;
    }
}
