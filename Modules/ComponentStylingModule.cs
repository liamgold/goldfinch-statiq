using Statiq.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Goldfinch.Modules
{
    public class ComponentStylingModule : ParallelModule
    {
        private const string ComponentStylingRegex = @"\[\[(.*?)\]\]";

        protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            var content = await input.GetContentStringAsync();

            var matches = Regex.Matches(content, ComponentStylingRegex);

            var cssLinks = new List<string>();
            var jsLinks = new List<string>();

            foreach (Match match in matches)
            {
                var link = match.Groups[1].Value.Split('|');

                switch (link[0])
                {
                    case "javascript":
                        if (!jsLinks.Contains(link[1]))
                        {
                            jsLinks.Add(link[1]);
                        }
                        break;
                    case "stylesheet":
                        if (!cssLinks.Contains(link[1]))
                        {
                            cssLinks.Add(link[1]);
                        }
                        break;
                    default:
                        break;
                }
            }

            var jsString = string.Join(string.Empty, jsLinks.Select(x => $"<script src=\"{x}\" defer></script>"));
            var cssString = string.Join(string.Empty, cssLinks.Select(x => $"<link rel=\"stylesheet\" href=\"{x}\" />"));

            var result = Regex.Replace(content, ComponentStylingRegex, string.Empty);

            result = result.Replace("[ComponentStyles]", $"{cssString}{jsString}");

            return input.Clone(context.GetContentProvider(result, MediaTypes.Html)).Yield();
        }
    }
}