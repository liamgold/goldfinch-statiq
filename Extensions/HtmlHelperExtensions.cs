using Goldfinch.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Goldfinch.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string GetUrl(this IHtmlHelper htmlHelper, string type, string urlSlug = null)
        {
            return type switch
            {
                BlogDetail.Codename => $"/blog/{urlSlug}",
                BlogListing.Codename => "/blog/",
                _ => "/",
            };
        }
    }
}