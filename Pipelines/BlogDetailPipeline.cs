using Goldfinch.Models;
using Kentico.Kontent.Delivery.Abstractions;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Minification;
using Statiq.Razor;

namespace Goldfinch.Pipelines
{
    public class BlogDetailPipeline : Pipeline
    {
        public BlogDetailPipeline(IDeliveryClient client)
        {
            InputModules = new ModuleList
            {
                new Kontent<BlogDetail>(client),
                new MergeContent(new ReadFiles("_BlogDetail.cshtml")),
            };

            ProcessModules = new ModuleList
            {
                new RenderRazor().WithModel(KontentConfig.As<BlogDetail>()),
                new SetDestination(Config.FromDocument((doc, ctx)  => new NormalizedPath($"blog/{doc.AsKontent<BlogDetail>().UrlSlug}.html" ))),
            };

            OutputModules = new ModuleList
            {
                new ComponentStylingModule(),
                new MinifyCss(),
                new WriteFiles(),
            };
        }
    }
}