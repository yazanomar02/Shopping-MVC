
using System.Globalization;
using System.Text.RegularExpressions;

namespace AmazonMVCApp.Constraints
{
    public class SlugConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // فحص ان كان لدي في الطلب سلوغ ام لا
            if(!values.TryGetValue("slug", out var slug))
                return false;


            // فحص هل هو null || empty
            var slugAsString = Convert.ToString(slug, CultureInfo.InvariantCulture); // CultureInfo.InvariantCulture للتعرف ان كان هناك احرف محروفة مثل الاحرف التركية
            
            if(string.IsNullOrEmpty(slugAsString))
                return false;


            // فحص هل هناك Regular expression نتقيد به
            return Regex.IsMatch(slugAsString, "^[a-zA-Z0-9- ]+$");
        }
    }
}
