using Goldfinch.Extensions;
using Goldfinch.Models;
using Goldfinch.Models.ViewModels;
using Kentico.Kontent.Delivery.Abstractions;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Minification;
using Statiq.Razor;

namespace Goldfinch.Pipelines
{
    public class BlogListingPipeline : Pipeline
    {
        public BlogListingPipeline(IDeliveryClient client)
        {
            Dependencies.AddRange(nameof(BlogDetailPipeline));

            InputModules = new ModuleList
            {
                new Kontent<BlogListing>(client),
                new MergeContent(new ReadFiles("Blog/_Listing.cshtml")),
            };

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(nameof(BlogDetailPipeline)),
                new PaginateDocuments(6),
                new RenderRazor()
                    .WithModel(Config.FromDocument((document, context) =>
                    {
                        var model = new BlogListViewModel(document.AsPagedKontent<BlogDetail>());
                        return model;
                    }
                )),
                new SetDestination(Config.FromDocument((doc, ctx) => Filename(doc))),
            };

            OutputModules = new ModuleList
            {
                new ComponentStylingModule(),
                new MinifyCss(),
                new WriteFiles(),
            };
        }

        private static NormalizedPath Filename(IDocument document)
        {
            var index = document.GetInt(Keys.Index);

            return new NormalizedPath($"{(index > 1 ? $"blog/{index}/" : "blog/")}index.html");
        }
    }
}