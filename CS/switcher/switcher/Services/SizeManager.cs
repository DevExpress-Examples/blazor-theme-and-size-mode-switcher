using DevExpress.Blazor;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.JSInterop;

namespace switcher.Services {
    public class SizeManager {
        protected CookiesService _cookiesService;
        protected IJSRuntime _jsRuntime { get; set; }
        const string SizeModeCookieKey = "DXSizeMode";
        
        public SizeMode ActiveSizeMode;

        public SizeManager(CookiesService cs, IHttpContextAccessor httpContextAccessor, IJSRuntime js) {
            _cookiesService = cs;
            _jsRuntime = js;
            var sizeMode = _cookiesService.GetCookie(httpContextAccessor, SizeModeCookieKey);
            if (string.IsNullOrEmpty(sizeMode))
                ActiveSizeMode = SizeMode.Medium;
            else
                ActiveSizeMode = Enum.Parse<SizeMode>(sizeMode);
        }

        public async Task SetSizeMode(SizeMode sizeMode) {
            ActiveSizeMode = sizeMode;
            await _cookiesService.SetCookie(SizeModeCookieKey, sizeMode.ToString());
            await SetSizeInJS();
        }
        public async Task SetSizeInJS() {
            await _jsRuntime.InvokeVoidAsync("setSize", GetFontSizeString());
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
