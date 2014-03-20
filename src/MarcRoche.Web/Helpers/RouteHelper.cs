using System.Web.Mvc;

public static class RouteHelper
{
    public static string IsRoute(this UrlHelper urlHelper, string route)
    {
        var routeValueDictionary = urlHelper.RequestContext.RouteData.Values;
        var rootUrl = urlHelper.Content("~/");
        var s = string.Format("{0}{1}/{2}/", rootUrl, routeValueDictionary["controller"], routeValueDictionary["action"]);

        if (s.ToLower().StartsWith(route))
        {
            return "class=selectedTab";
        }
        else
        {
            return "class=unselectedTab";
        }
    }
}