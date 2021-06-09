using Goldfinch.Extensions;
using Goldfinch.Models;
using Goldfinch.Models.ViewModels;
using Goldfinch.Modules;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Razor;

namespace Goldfinch.Pipelines
{
    public class BlogListingPipeline : Pipeline
    {
        private BlogListing _blogListing;

        public BlogListingPipeline(IDeliveryClient client)
        {
            // We need the blog post documents from BlogDetailPipeline, so a pipeline dependency has been added to ensure it has ran.
            Dependencies.AddRange(nameof(BlogDetailPipeline));

            InputModules = new ModuleList
            {
                // Retrieve and store the blog listing document so we can use it in the BlogListViewModel constructor.
                // Process module list currently replaces the input documents with the documents from BlogDetailPipeline.
                new Kontent<BlogListing>(client).WithQuery(new LimitParameter(1)),
                new ExecuteConfig(Config.FromDocument((doc, ctx) =>
                {
                    _blogListing = doc.AsKontent<BlogListing>();
                })),
            };

            ProcessModules = new ModuleList
            {
                // Paginate documents from the BlogDetailPipeline.
                new ReplaceDocuments(nameof(BlogDetailPipeline)),
                new PaginateDocuments(6),

                new MergeContent(new ReadFiles("Blog/_Listing.cshtml")),
                new SetDestination(Config.FromDocument((doc, ctx) => Filename(doc))),
                new RenderRazor()
                    .WithModel(Config.FromDocument((doc, ctx) =>
                    {
                        return new BlogListViewModel(doc.AsPagedKontent<BlogDetail>(), _blogListing);
                    }
                )),
            };

            PostProcessModules = new ModuleList
            {
                new ComponentStylingModule(),
            };

            OutputModules = new ModuleList
            {
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