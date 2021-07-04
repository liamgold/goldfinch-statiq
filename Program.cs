using Goldfinch.Models.ContentTypes;
using Goldfinch.Resolvers;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Statiq.App;
using Statiq.Common;
using Statiq.Web;
using System.Threading.Tasks;

namespace Goldfinch
{
    public class Program
    {
        public static async Task<int> Main(string[] args) =>
          await Bootstrapper
            .Factory
            .CreateWeb(args)
            .ConfigureServices((services, config) =>
            {
                // Add the type provider
                services.AddSingleton<ITypeProvider, CustomTypeProvider>();
                services.AddSingleton<IContentLinkUrlResolver, CustomContentLinkUrlResolver>();
                // Configure Delivery SDK
                services.AddDeliveryClient((IConfiguration)config);
            })
            .AddProcess(ProcessTiming.Initialization, _ => new ProcessLauncher("npm", "install") { LogErrors = false })
            .AddProcess(ProcessTiming.AfterExecution, _ => new ProcessLauncher("npm", "run", "build") { LogErrors = false })
            .RunAsync();
    }
}