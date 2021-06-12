using Goldfinch.Models;
using Kentico.Kontent.Delivery.Abstractions;
using System.Threading.Tasks;

namespace Goldfinch.Resolvers
{
    public class CustomContentLinkUrlResolver : IContentLinkUrlResolver
    {
        public Task<string> ResolveLinkUrlAsync(IContentLink link)
        {
            return Task.FromResult(link.ContentTypeCodename switch
            {
                Home.Codename => "/",
                BlogDetail.Codename => $"/blog/{link.UrlSlug}",
                BlogListing.Codename => "/blog/",
                _ => "/",
            });
        }

        public Task<string> ResolveBrokenLinkUrlAsync()
        {
            return Task.FromResult("/404");
        }
    }
}
