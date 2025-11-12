using DevExpress.Blazor;

namespace switcher.Services {
    public class SizeManager {
        protected CookiesService _cookiesService;
        const string SizeModeCookieKey = "DXSizeMode";
        
        public SizeMode ActiveSizeMode;

        public SizeManager(CookiesService cs, IHttpContextAccessor httpContextAccessor) {
            _cookiesService = cs;
            var sizeMode = _cookiesService.GetCookie(httpContextAccessor, SizeModeCookieKey);
            if (string.IsNullOrEmpty(sizeMode))
                ActiveSizeMode = SizeMode.Medium;
            else
                ActiveSizeMode = Enum.Parse<SizeMode>(sizeMode);
        }

        public async Task SetSizeMode(SizeMode sizeMode) {
            ActiveSizeMode = sizeMode;
            await _cookiesService.SetCookie(SizeModeCookieKey, sizeMode.ToString());
        }

        public string GetFontSizeString() {
            return ActiveSizeMode switch {
                SizeMode.Small => "14px",
                SizeMode.Medium => "16px",
                SizeMode.Large => "18px",
                _ => "16px"
            };
        }
    }
}
