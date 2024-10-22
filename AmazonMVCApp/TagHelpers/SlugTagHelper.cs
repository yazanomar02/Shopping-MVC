using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace AmazonMVCApp.TagHelpers
{

    [HtmlTargetElement("url-with-slug")]
    public class SlugTagHelper : AnchorTagHelper
    {
        public SlugTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }



        [HtmlAttributeName("for-product-id")]
        public Guid ProductId { get; set; }


        [HtmlAttributeName("for-product-name")]
        public string ProductName { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;

            //var slug = Regex.Replace(
            //    ProductName, "[^a-zA-Z0-9- ]+", " ").Trim().Replace(" ", "-").ToLower();

            var slug = ProductName.Replace(" ", "-").ToLower();

            RouteValues.Add("slug", slug);
            RouteValues.Add("productId", ProductId.ToString());


            base.Process(context, output);
        }
    }
}
