using Goldfinch.Models;
using Goldfinch.Modules;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Urls.QueryParameters;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
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
            };

            ProcessModules = new ModuleList
            {
                new MergeContent(new ReadFiles("Home/_Home.cshtml")),
                new SetDestination(new NormalizedPath("index.html")),
                new RenderRazor().WithModel(KontentConfig.As<Home>()),
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