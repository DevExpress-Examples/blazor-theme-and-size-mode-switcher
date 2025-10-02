using DevExpress.Blazor;
using Microsoft.JSInterop;
using System.Text.Json;

namespace switcher.Services {
    public class ThemesService {
        protected CookiesService _cookiesService;
        protected IThemeChangeService _dxThemeChangeService;
        protected IHttpContextAccessor _httpContextAccessor;

        public ITheme ActiveTheme { get; private set; }
        public ITheme DefaultTheme { get; } = ThemesCollection.FluentLight;
        public const string ThemeCookieKey = "DXCurrentTheme";

        public ThemesService(CookiesService cs, IThemeChangeService dxThemeService, IHttpContextAccessor httpContextAccessor) {
            _cookiesService = cs;
            _dxThemeChangeService = dxThemeService;
            _httpContextAccessor = httpContextAccessor;
            ActiveTheme = GetThemeFromCookies(httpContextAccessor);
        }

        public ITheme GetThemeFromCookies(IHttpContextAccessor httpContextAccessor) {
            var cookieValue = _cookiesService.GetCookie(httpContextAccessor, ThemeCookieKey);
            if (string.IsNullOrEmpty(cookieValue))
                return DefaultTheme;

            ThemeCookie? cookie = null;
            try {
                cookie = JsonSerializer.Deserialize<ThemeCookie>(cookieValue);
            }
            catch {
                return cookieValue.GetTheme();
            }

            if (cookie == null || string.IsNullOrEmpty(cookie.ThemeName))
                return DefaultTheme;

            var theme = cookie.ThemeName.GetTheme();

            if (!string.IsNullOrEmpty(cookie.AccentColor) &&
                (cookie.ThemeName == nameof(MyTheme.Fluent_Light) || cookie.ThemeName == nameof(MyTheme.Fluent_Dark))) {
                return ApplyFluentAccent(cookie.ThemeName, cookie.AccentColor);
            }

            return theme;
        }

        public async Task SetActiveThemeAsync(MyTheme theme, string? accentColor = null) {
            string themeName = Enum.GetName(typeof(MyTheme), theme);

            var cookie = new ThemeCookie(themeName, accentColor);
            var cookieJson = JsonSerializer.Serialize(cookie);
            await _cookiesService.SetCookie(ThemeCookieKey, cookieJson);

            ITheme appliedTheme = (!string.IsNullOrEmpty(accentColor) &&
                                   (theme == MyTheme.Fluent_Light || theme == MyTheme.Fluent_Dark))
                                 ? ApplyFluentAccent(themeName, accentColor)
                                 : theme.GetTheme();

            ActiveTheme = appliedTheme;
            await _dxThemeChangeService.SetTheme(appliedTheme);
        }

        private static ITheme ApplyFluentAccent(string themeName, string accentColor) {
            var mode = themeName == nameof(MyTheme.Fluent_Dark) ? ThemeMode.Dark : ThemeMode.Light;
            return Themes.Fluent.Clone(properties => {
                properties.Mode = mode;
                properties.AddFilePaths("css/theme-fluent.css");
                properties.SetCustomAccentColor(accentColor);
            });
        }

        private record ThemeCookie(string ThemeName, string? AccentColor);
    }

    public static class Extensions {
        public static ITheme GetTheme(this MyTheme theme) => theme switch {
            MyTheme.Fluent_Light => ThemesCollection.FluentLight,
            MyTheme.Fluent_Dark => ThemesCollection.FluentDark,
            MyTheme.Blazing_Berry => ThemesCollection.BlazingBerry,
            MyTheme.Blazing_Dark => ThemesCollection.BlazingDark,
            MyTheme.Purple => ThemesCollection.Purple,
            MyTheme.Office_White => ThemesCollection.OfficeWhite,
            MyTheme.Bootstrap => ThemesCollection.BootstrapDefault,
            _ => ThemesCollection.FluentLight
        };

        public static ITheme GetTheme(this string themeName) {
            return Enum.TryParse<MyTheme>(themeName, out var result)
                ? result.GetTheme()
                : ThemesCollection.FluentLight;
        }
    }
}
