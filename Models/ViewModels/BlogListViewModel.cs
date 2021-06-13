using Goldfinch.Extensions;
using Goldfinch.Models.ContentTypes;
using Statiq.Common;
using System.Linq;

namespace Goldfinch.Models.ViewModels
{
    public class BlogListViewModel : BaseViewModel
    {
        public BlogListViewModel(IDocument document, BlogListing blogListing) : base(document)
        {            
            BlogListing = blogListing;
            PagedContent = document.AsPagedKontent<BlogDetail>();
            SeoData = new SeoData
            {
                Title = BlogListing.SeoMetaTitle,
                Description = BlogListing.SeoMetaDescription,
                Image = BlogListing.BaseTeaserImage?.FirstOrDefault()?.Url,
                Url = document.GetLink(true)
            };
        }

        public BlogListing BlogListing { get; }
        public PagedContent<BlogDetail> PagedContent { get; }
    }
}
