using System.Text.RegularExpressions;

namespace AmazonMVCApp.Transformers
{
    public class SlugOParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            // check if the slug parameter os string
            if(value is not string)
                return null;


            //repleace all unmatched chars with ( - ) then trim the final result
            var result = Regex.Replace(
                value.ToString(),
                "[^a-zA-Z0-9- ]+",
                "-",
                RegexOptions.CultureInvariant,
                TimeSpan.FromMicroseconds(500))
                .Trim('-');
                   
            return result;
        }
    }
}
