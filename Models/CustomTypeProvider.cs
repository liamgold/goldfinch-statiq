using System;
using System.Collections.Generic;
using System.Linq;
using Kentico.Kontent.Delivery.Abstractions;

namespace Goldfinch.Models
{
    public class CustomTypeProvider : ITypeProvider
    {
        private static readonly Dictionary<Type, string> _codenames = new Dictionary<Type, string>
        {
            {typeof(BlogDetail), "blog_detail"},
            {typeof(BlogListing), "blog_listing"},
            {typeof(CodeBlock), "code_block"},
            {typeof(Error), "error"},
            {typeof(Home), "home"},
            {typeof(Image), "image"}
        };

        public Type GetType(string contentType)
        {
            return _codenames.Keys.FirstOrDefault(type => GetCodename(type).Equals(contentType));
        }

        public string GetCodename(Type contentType)
        {
            return _codenames.TryGetValue(contentType, out var codename) ? codename : null;
        }
    }
}