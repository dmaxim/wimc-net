using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Wimc.TagHelpers
{
    public class IconActionTagHelper : AnchorTagHelper
    {
        public IconActionTagHelper(IHtmlGenerator generator) : base(generator)
        {
            
        }
        
        public string IconClass { get; set; }
        
        public string AlternateText { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.PreContent.SetHtmlContent($"<i title='{AlternateText}' class='{IconClass}'>");
            output.PostContent.SetHtmlContent("</i>");
            base.Process(context, output);
        }
    }
}