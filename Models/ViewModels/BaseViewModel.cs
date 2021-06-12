using Statiq.Common;

namespace Goldfinch.Models.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel(IDocument document)
        {
            CurrentUrl = document.GetLink(true);
        }

        public string CurrentUrl { get; }
    }
}
