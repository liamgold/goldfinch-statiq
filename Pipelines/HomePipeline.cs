using Goldfinch.Models;
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
                // Load "Home" item and transfer it into IDocument.
                // <see href="https://github.com/alanta/Kontent.Statiq">Kontent.Statiq</see>
                new Kontent<Home>(client)
                    .WithQuery(
                        new LimitParameter(1)
                    ),
                // Load Razor template to IDocument content.
                // <see href="https://github.com/statiqdev/Statiq.Framework/blob/main/src/core/Statiq.Core/Modules/Content/MergeContent.cs">MergeContent</see>.
                // <see href="https://statiq.dev/web/content-and-data/content/">Content propery of IDocument</see>
                new MergeContent(
                    new ReadFiles(patterns: "_Home.cshtml")
                )
            };

            ProcessModules = new ModuleList
            {
                // Render HTML file from Razor template and document typed as Hero model.
                // <see href="https://github.com/statiqdev/Statiq.Framework/blob/main/src/extensions/Statiq.Razor/RenderRazor.cs">RenderRazor</see>
                new RenderRazor()
                    .WithModel(KontentConfig.As<Home>()),

                // Set file system destionation for the document.
                // <see href="https://github.com/statiqdev/Statiq.Framework/blob/main/src/core/Statiq.Core/Modules/IO/SetDestination.cs">SetDestination</see>
                new SetDestination(new NormalizedPath("index.html")),
            };

            OutputModules = new ModuleList
            {
                // Flush the the output.
                // <see href="https://github.com/statiqdev/Statiq.Framework/blob/main/src/core/Statiq.Core/Modules/IO/WriteFiles.cs">WriteFiles</see>.
                new WriteFiles()
            };
        }
    }
}