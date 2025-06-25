using Microsoft.JSInterop;

namespace switcher.Services {
    public class CookiesService {
        protected IJSRuntime JSRuntime { get; set; }
        public CookiesService(IJSRuntime js) {
            JSRuntime = js;
        }
        public string GetCookie(IHttpContextAccessor httpContextAccessor, string key) {
            return httpContextAccessor.HttpContext?.Request.Cookies[key];
        }
        public async Task SetCookie(string key, string value) { 
            await JSRuntime.InvokeVoidAsync("setCookie", key, value);
        }
    }
}
