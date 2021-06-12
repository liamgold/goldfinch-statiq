using Kontent.Statiq;
using Statiq.Common;

namespace Goldfinch.Models.ViewModels
{
    public class ErrorViewModel : BaseViewModel
    {
        public ErrorViewModel(IDocument document) : base(document)
        {
            Error = document.AsKontent<Error>();
        }

        public Error Error { get; }
    }
}
