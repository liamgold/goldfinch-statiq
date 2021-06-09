namespace Goldfinch.Models.ViewModels
{
    public class BlogListViewModel
    {
        public BlogListViewModel(PagedContent<BlogDetail> pagedContent, BlogListing blogListing = null)
        {
            BlogListing = blogListing;
            PagedContent = pagedContent;
        }

        public BlogListing BlogListing { get; }
        public PagedContent<BlogDetail> PagedContent { get; }
    }
}
