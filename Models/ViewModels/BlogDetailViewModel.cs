using Kontent.Statiq;
using Statiq.Common;

namespace Goldfinch.Models.ViewModels
{
    public class BlogDetailViewModel : BaseViewModel
    {
        public BlogDetailViewModel(IDocument document) : base(document)
        {
            BlogDetail = document.AsKontent<BlogDetail>();
        }

        public BlogDetail BlogDetail { get; }
    }
}
