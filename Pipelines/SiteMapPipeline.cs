using Goldfinch.Modules;
using Kontent.Statiq;
using Statiq.Common;
using Statiq.Core;
using System;
using System.Linq;

namespace Goldfinch.Pipelines
{
    public class SiteMapPipeline : Pipeline
    {
        public SiteMapPipeline()
        {
            Dependencies.AddRange(
                nameof(BlogDetailPipeline),
                nameof(BlogListingPipeline),
                nameof(HomePipeline)
            );

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(Dependencies.ToArray()),

                new SetMetadata(Keys.SitemapItem, Config.FromDocument((doc, ctx) =>
                {
                    var siteMapItem = new SitemapItem(doc.Destination.FullPath)
                    {
                        LastModUtc = doc.Get<DateTime?>(KontentKeys.System.LastModified, null)
                    };

                    if (!siteMapItem.LastModUtc.HasValue)
                    {
                        siteMapItem.LastModUtc = DateTime.UtcNow;
                        siteMapItem.ChangeFrequency = SitemapChangeFrequency.Weekly;
                    }
                    else
                    {
                        siteMapItem.ChangeFrequency = SitemapChangeFrequency.Monthly;
                    }

                    return siteMapItem;
                })),

                new CustomGenerateSitemap(),
            };

            OutputModules = new ModuleList
            {
                new WriteFiles(),
            };
        }
    }
}