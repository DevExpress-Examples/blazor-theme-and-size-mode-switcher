using DevExpress.Blazor;

namespace switcher.Services {
    public class ThemesService {
        protected CookiesService _cookiesService;
        protected IThemeChangeService _dxThemeChangeService;

        public const string ThemeCookieKey = "DXCurrentTheme";

        public ThemesService(CookiesService cs, IThemeChangeService dxThemeService) {
            _cookiesService = cs;
            _dxThemeChangeService = dxThemeService;
        }

        public ITheme ActiveTheme => _dxThemeChangeService.ActiveTheme;

        public ITheme GetThemeFromCookies(IHttpContextAccessor httpContextAccessor) {
            var themeName = _cookiesService.GetCookie(httpContextAccessor, ThemeCookieKey);
            
            if(!string.IsNullOrEmpty(themeName))
                return ThemesCollection.GetTheme(themeName);

            return null;
        }
        public async Task SetActiveThemeAsync(ITheme theme) {
            await _cookiesService.SetCookie(ThemeCookieKey, theme.Name);
            await _dxThemeChangeService.SetTheme(theme);
        }
    }
}
