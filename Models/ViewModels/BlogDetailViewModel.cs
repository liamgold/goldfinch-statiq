using Goldfinch.Models.ContentTypes;
using Kontent.Statiq;
using Statiq.Common;
using System.Linq;

namespace Goldfinch.Models.ViewModels
{
    public class BlogDetailViewModel : BaseViewModel
    {
        public BlogDetailViewModel(IDocument document) : base(document)
        {
            BlogDetail = document.AsKontent<BlogDetail>();
            SeoData = new SeoData
            {
                Title = BlogDetail.SeoMetaTitle,
                Description = BlogDetail.SeoMetaDescription,
                IsArticle = true,
                Image = BlogDetail.BaseTeaserImage?.FirstOrDefault()?.Url,
                Url = document.GetLink(true)
            };
        }

        public BlogDetail BlogDetail { get; }
    }
}
