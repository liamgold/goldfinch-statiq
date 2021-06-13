using Goldfinch.Models.ContentTypes;
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
    public class NotFoundPipeline : Pipeline
    {
        public NotFoundPipeline(IDeliveryClient client)
        {
            InputModules = new ModuleList
            {
                new Kontent<Error>(client).WithQuery(new LimitParameter(1)),
            };

            ProcessModules = new ModuleList
            {
                new MergeContent(new ReadFiles("Error/_NotFound.cshtml")),
                new SetDestination(new NormalizedPath("404.html")),
                new RenderRazor()
                    .WithModel(Config.FromDocument((doc, ctx) =>
                    {
                        return new ErrorViewModel(doc);
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