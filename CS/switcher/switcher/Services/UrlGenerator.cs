using System.Web;

namespace switcher;

public static class UrlGenerator {
    public const string ToggleSidebarName = "toggledSidebar";

    public static string GetUrl(string baseUrl, bool toggledSidebar) {
        return $"{baseUrl}?{ToggleSidebarName}={toggledSidebar}";
    }
    public static string GetUrl(bool toggledSidebar, string returnUrl) {
        var baseUriBuilder = new UriBuilder(returnUrl);
        var query = HttpUtility.ParseQueryString(baseUriBuilder.Query);
        var baseUrl = baseUriBuilder.Fragment + baseUriBuilder.Host + baseUriBuilder.Path;

        return $"{baseUrl}?{ToggleSidebarName}={toggledSidebar}&{query}";
    }
}
