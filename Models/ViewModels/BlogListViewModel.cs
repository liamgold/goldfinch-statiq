using Goldfinch.Extensions;
using Statiq.Common;

namespace Goldfinch.Models.ViewModels
{
    public class BlogListViewModel : BaseViewModel
    {
        public BlogListViewModel(IDocument document, BlogListing blogListing) : base(document)
        {            
            BlogListing = blogListing;
            PagedContent = document.AsPagedKontent<BlogDetail>();
        }

        public BlogListing BlogListing { get; }
        public PagedContent<BlogDetail> PagedContent { get; }
    }
}
