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
    public class BlogDetailPipeline : Pipeline
    {
        public BlogDetailPipeline(IDeliveryClient client)
        {
            InputModules = new ModuleList
            {
                new Kontent<BlogDetail>(client).WithQuery(new OrderParameter($"elements.{BlogDetail.PostDateCodename}", SortOrder.Descending)),
            };

            ProcessModules = new ModuleList
            {
                new MergeContent(new ReadFiles("Blog/_Detail.cshtml")),
                new SetDestination(Config.FromDocument((doc, ctx)  => new NormalizedPath($"blog/{doc.AsKontent<BlogDetail>().UrlSlug}.html" ))),
                new RenderRazor()
                    .WithModel(Config.FromDocument((doc, ctx) =>
                    {
                        return new BlogDetailViewModel(doc);
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
    }
}