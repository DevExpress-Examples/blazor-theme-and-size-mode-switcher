using DevExpress.Blazor;

namespace switcher.Services {
    public class ThemesService {
        protected CookiesService _cookiesService;
        protected IThemeChangeService _dxThemeChangeService;

        public const string ThemeCookieKey = "DXCurrentTheme";
        public ITheme ActiveTheme { get; private set; }
        public ITheme DefaultTheme { get; private set; } = ThemesCollection.FluentLight;

        public ThemesService(CookiesService cs, IThemeChangeService dxThemeSerice) {
            _cookiesService = cs;
            _dxThemeChangeService = dxThemeSerice;
        }

        public ITheme GetThemeFromCookies(IHttpContextAccessor httpContextAccessor) {
            var themeName = _cookiesService.GetCookie(httpContextAccessor, ThemeCookieKey);
            var iTheme = string.IsNullOrEmpty(themeName) ? DefaultTheme : themeName.GetTheme();
            ActiveTheme = iTheme;
            return iTheme;
        }
        public async Task SetActiveThemeAsync(MyTheme theme) {
            var themeName = Enum.GetName(typeof(MyTheme), theme);
            await _cookiesService.SetCookie(ThemeCookieKey, themeName);

            var iTheme = theme.GetTheme();
            ActiveTheme = iTheme;
            await _dxThemeChangeService.SetTheme(ActiveTheme);
        }
    }

    public static class Extensions {
        public static ITheme GetTheme(this MyTheme theme) {
            return theme switch {
                MyTheme.Fluent_Light => ThemesCollection.FluentLight,
                MyTheme.Fluent_Dark => ThemesCollection.FluentDark,
                MyTheme.Blazing_Berry => ThemesCollection.BlazingBerry,
                MyTheme.Blazing_Dark => ThemesCollection.BlazingDark,
                MyTheme.Purple => ThemesCollection.Purple,
                MyTheme.Office_White => ThemesCollection.OfficeWhite,
                MyTheme.Bootstrap => ThemesCollection.BootstrapDefault
            };
        }
        public static ITheme GetTheme(this string themeName) {
            var theme = MyTheme.Fluent_Light;
            if (Enum.TryParse<MyTheme>(themeName, out MyTheme result))
                theme = result;
            return theme.GetTheme();
        }
    }
}
