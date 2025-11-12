using DevExpress.Blazor;
using Microsoft.JSInterop;
using System.Text.Json;

namespace switcher.Services {
    public class ThemesService {
        protected CookiesService _cookiesService;
        protected IThemeChangeService _dxThemeChangeService;

        public ITheme ActiveTheme { get => _dxThemeChangeService.ActiveTheme; }
        public ITheme DefaultTheme { get; } = ThemesCollection.FluentLight();
        public const string ThemeCookieKey = "DXCurrentTheme";

        public ThemesService(CookiesService cs, IThemeChangeService dxThemeService, IHttpContextAccessor httpContextAccessor) {
            _cookiesService = cs;
            _dxThemeChangeService = dxThemeService;
        }

        public ITheme GetThemeFromCookies(IHttpContextAccessor httpContextAccessor) {
            var cookieValue = _cookiesService.GetCookie(httpContextAccessor, ThemeCookieKey);
            ThemeCookie? cookie = cookieValue.TryDeserialize();
            if (cookie == null || string.IsNullOrEmpty(cookie.ThemeName))
                return DefaultTheme;

            return cookie.GetTheme();
        }

        public async Task SetActiveThemeAsync(ITheme theme, string? accentColor = null) {
            var nameWithoutAccent = theme.Name.Split('_')[0];
            var cookie = new ThemeCookie(nameWithoutAccent, accentColor);
            await _cookiesService.SetCookie(ThemeCookieKey, JsonSerializer.Serialize(cookie));

            var newTheme = nameWithoutAccent.GetTheme(accentColor);
            await _dxThemeChangeService.SetTheme(newTheme);
        }

    }
    public record ThemeCookie(string ThemeName, string? AccentColor);

    public static class Extensions {
        public static ITheme GetTheme(this MyTheme theme, string? accent = null) => theme switch {
            MyTheme.FluentLight => ThemesCollection.FluentLight(accent),
            MyTheme.FluentDark => ThemesCollection.FluentDark(accent),
            MyTheme.BlazingBerry => ThemesCollection.BlazingBerry,
            MyTheme.BlazingDark => ThemesCollection.BlazingDark,
            MyTheme.Purple => ThemesCollection.Purple,
            MyTheme.OfficeWhite => ThemesCollection.OfficeWhite,
            MyTheme.Bootstrap => ThemesCollection.BootstrapDefault,
            _ => ThemesCollection.FluentLight()
        };

        public static ITheme GetTheme(this string themeName, string? accent = null) {
            return Enum.TryParse<MyTheme>(themeName, out var result)
                ? result.GetTheme(accent)
                : ThemesCollection.FluentLight();
        }
        public static ITheme GetTheme(this ThemeCookie cookie) {
            return cookie.ThemeName.GetTheme(cookie.AccentColor);
        }

        public static ThemeCookie TryDeserialize(this string s) {
            if (string.IsNullOrEmpty(s))
                return null;
            ThemeCookie? cookie = null;
            try {
                cookie = JsonSerializer.Deserialize<ThemeCookie>(s);
            }
            catch {}
            return cookie;
        }
    }
}
