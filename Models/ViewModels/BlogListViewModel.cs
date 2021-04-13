namespace Goldfinch.Models.ViewModels
{
    public class BlogListViewModel
    {
        public BlogListViewModel(PagedContent<BlogDetail> pagedContent)
        {
            PagedContent = pagedContent;
        }

        public BlogListing BlogListing { get; }
        public PagedContent<BlogDetail> PagedContent { get; }
    }
}
