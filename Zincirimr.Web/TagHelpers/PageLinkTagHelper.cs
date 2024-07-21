using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Zincirimr.Web.ViewModels;

namespace Zincirimr.Web.TagHelpers
{
    [HtmlTargetElement("div",Attributes = "page-model")]
    public class PageLinkTagHelper:TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }
        [ViewContext]
        public ViewContext? ViewContext { get; set; }
        public PageInfo? PageModel { get; set; }
        public string? PageAction { get; set; }
        public string? PageClassActive { get; set; }
        public string? PageClassLink { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext!=null&&PageModel!=null&&PageAction!=null)
            {
                TagBuilder div = new TagBuilder("div");
                var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                for (int i = 1; i <= PageModel.TotalPages; i++)
                {

                    TagBuilder a = new TagBuilder("a");
                    a.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });
                    a.AddCssClass(i==PageModel.CurrentPage?PageClassLink:PageClassLink);
                    a.InnerHtml.Append(i.ToString());
                    div.InnerHtml.AppendHtml(a);
                }

                output.Content.AppendHtml(div);
            }
        }
    }
}
