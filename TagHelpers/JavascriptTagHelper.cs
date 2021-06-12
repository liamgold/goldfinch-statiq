using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Goldfinch.TagHelpers
{
    public class JavascriptTagHelper : TagHelper
    {
        [HtmlAttributeName("file")]
        public string File { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            output.PostContent.SetContent($"[[javascript|/assets/scripts/{File}.js]]");
        }
    }
}
