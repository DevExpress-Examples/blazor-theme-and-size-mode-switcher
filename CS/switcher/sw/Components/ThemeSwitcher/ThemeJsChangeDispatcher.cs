using DevExpress.Blazor;
using DevExpress.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using sw.Services;

public class ThemeJsChangeDispatcher : ComponentBase, IThemeChangeRequestDispatcher, IAsyncDisposable {
    [Parameter] public string InitialThemeName { get; set; }

    [Inject] private ISafeJSRuntime JsRuntime { get; set; }

    [Inject] private DxThemesService ThemesService { get; set; }
    [Inject] protected IThemeChangeService DxThemesService { get; set; }

    private Theme _pendingTheme;
    private IJSObjectReference _module;

    protected override void OnInitialized() {
        base.OnInitialized();
        ThemesService.ThemeChangeRequestDispatcher = this;
        if(ThemesService.ActiveTheme == null)
            ThemesService.SetActiveThemeByName(InitialThemeName);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender) {
        await base.OnAfterRenderAsync(firstRender);

        if(firstRender)
            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./switcher-resources/js/theme-controller.js");
    }

    public async void RequestThemeChange(Theme theme) {
        if(_pendingTheme == theme) return;
        _pendingTheme = theme;

        await DxThemesService.SetTheme(theme.CurrentTheme);

        await _module.InvokeVoidAsync("ThemeController.switchBsThemeMode", theme.BootstrapThemeMode, DotNetObjectReference.Create(this));
        await JsRuntime.InvokeVoidAsync("PageHelper.themes.setThemeName", sw.Services.DxThemesService.ThemeCookieKey, theme.Name);
    }

    [JSInvokable]
    public async Task ThemeLoadedAsync() {
        if(ThemesService.ThemeLoadNotifier != null) {
            await ThemesService.ThemeLoadNotifier.NotifyThemeLoadedAsync(_pendingTheme);
        }

        _pendingTheme = null;
    }

    public async ValueTask DisposeAsync() {
        try {
            if(_module != null)
                await _module.DisposeAsync();
        } catch(JSDisconnectedException) { }
    }
}
