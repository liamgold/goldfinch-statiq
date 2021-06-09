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

            foreach (Match match in matches)
            {
                var cssLink = match.Groups[1].Value;

                if (!cssLinks.Contains(cssLink))
                {
                    cssLinks.Add(cssLink);
                }
            }

            var cssString = string.Join(string.Empty, cssLinks.Select(x => $"<link rel=\"stylesheet\" href=\"{x}\" />"));

            var result = Regex.Replace(content, ComponentStylingRegex, string.Empty);

            result = result.Replace("[ComponentStyles]", cssString);

            return input.Clone(context.GetContentProvider(result, MediaTypes.Html)).Yield();
        }
    }
}