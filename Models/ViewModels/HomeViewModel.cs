using Kontent.Statiq;
using Statiq.Common;

namespace Goldfinch.Models.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel(IDocument document) : base(document)
        {
            Home = document.AsKontent<Home>();
        }

        public Home Home { get; }
    }
}
