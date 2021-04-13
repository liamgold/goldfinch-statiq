using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Goldfinch.TagHelpers
{
    public class StylesheetTagHelper : TagHelper
    {
        [HtmlAttributeName("file")]
        public string File { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            output.PostContent.SetContent($"[[/styles/{File}.css]]");
        }
    }
}
