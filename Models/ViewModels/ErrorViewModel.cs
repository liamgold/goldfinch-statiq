using Goldfinch.Models.ContentTypes;
using Kontent.Statiq;
using Statiq.Common;
using System.Linq;

namespace Goldfinch.Models.ViewModels
{
    public class ErrorViewModel : BaseViewModel
    {
        public ErrorViewModel(IDocument document) : base(document)
        {
            Error = document.AsKontent<Error>();
            SeoData = new SeoData
            {
                Title = Error.SeoMetaTitle,
                Description = Error.SeoMetaDescription,
                Image = Error.BaseTeaserImage?.FirstOrDefault()?.Url,
                Url = document.GetLink(true)
            };
        }

        public Error Error { get; }
    }
}
