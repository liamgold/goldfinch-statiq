using Goldfinch.Models;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Minification;
using Statiq.Razor;

namespace Goldfinch.Pipelines
{
    public class HomePipeline : Pipeline
    {
        public HomePipeline(IDeliveryClient client)
        {
            InputModules = new ModuleList
            {
                new Kontent<Home>(client).WithQuery(new LimitParameter(1)),
                new MergeContent(new ReadFiles("Home/_Home.cshtml")),
            };

            ProcessModules = new ModuleList
            {
                new RenderRazor().WithModel(KontentConfig.As<Home>()),
                new SetDestination(new NormalizedPath("index.html")),
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