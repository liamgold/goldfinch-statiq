using Goldfinch.Models.ContentTypes;
using Kontent.Statiq;
using Statiq.Common;
using System.Linq;

namespace Goldfinch.Models.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(IDocument document) : base(document)
        {
            Home = document.AsKontent<Home>();
            SeoData = new SeoData
            {
                Title = Home.SeoMetaTitle,
                Description = Home.SeoMetaDescription,
                Image = Home.BaseTeaserImage?.FirstOrDefault()?.Url,
                Url = document.GetLink(true)
            };
        }

        public Home Home { get; }
    }
}
