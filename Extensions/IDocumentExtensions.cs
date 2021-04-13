using Goldfinch.Models;
using Statiq.Common;

namespace Goldfinch.Extensions
{
    public static class IDocumentExtensions
    {
        public static PagedContent<TModel> AsPagedKontent<TModel>(this IDocument document)
        {
            return new PagedContent<TModel>(document);
        }
    }
}
