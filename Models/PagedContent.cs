using Kontent.Statiq;
using Statiq.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Goldfinch.Models
{
    public class PagedContent<TContentModel>
    {
        private readonly IDocument _document;
        private readonly Lazy<IReadOnlyList<TContentModel>> _children;
        private readonly Lazy<PagedContent<TContentModel>> _previous;
        private readonly Lazy<PagedContent<TContentModel>> _next;

        public PagedContent(IDocument document)
        {
            _document = document;
            _children = new Lazy<IReadOnlyList<TContentModel>>(() => document.GetChildren().Select(c => c.AsKontent<TContentModel>()).ToArray());

            _previous = new Lazy<PagedContent<TContentModel>>(() =>
            {
                var previous = document.GetDocument(Keys.Previous);
                return previous is not null ? new PagedContent<TContentModel>(previous) : null;
            });

            _next = new Lazy<PagedContent<TContentModel>>(() =>
            {
                var next = document.GetDocument(Keys.Next);
                return next is not null ? new PagedContent<TContentModel>(next) : null;
            });
        }

        public int Index => _document.GetInt(Keys.Index);

        public int TotalPages => _document.GetInt(Keys.TotalPages);

        public int TotalItems => _document.GetInt(Keys.TotalItems);

        public IReadOnlyList<TContentModel> Items => _children.Value;

        public PagedContent<TContentModel> Previous => _previous.Value;

        public PagedContent<TContentModel> Next => _next.Value;

        public string Url => _document.GetLink();
    }
}
