using DevExpress.Blazor;
using DevExpress.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace switcher.ThemeSwitcher;

public class ThemeJsChangeDispatcher : ComponentBase, IThemeChangeRequestDispatcher, IAsyncDisposable {
    [Parameter] public string InitialThemeName { get; set; }

    [Inject] private ISafeJSRuntime JsRuntime { get; set; }

    [Inject] private ThemeService Themes { get; set; }
    [Inject] protected IThemeChangeService DxThemesService { get; set; }

    private Theme _pendingTheme;
    private IJSObjectReference _module;

    protected override void OnInitialized() {
        base.OnInitialized();
        Themes.ThemeChangeRequestDispatcher = this;
        if(Themes.ActiveTheme == null)
            Themes.SetActiveThemeByName(InitialThemeName);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender) {
        await base.OnAfterRenderAsync(firstRender);

        if(firstRender)
            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./switcher-resources/theme-controller.js");
    }

    public async void RequestThemeChange(Theme theme) {
        if(_pendingTheme == theme) return;
        _pendingTheme = theme;

        await DxThemesService.SetTheme(theme.CurrentTheme);

        await _module.InvokeVoidAsync("ThemeController.switchBsThemeMode", theme.BootstrapThemeMode, DotNetObjectReference.Create(this));
    }

    [JSInvokable]
    public async Task ThemeLoadedAsync() {
        if(Themes.ThemeLoadNotifier != null) {
            await Themes.ThemeLoadNotifier.NotifyThemeLoadedAsync(_pendingTheme);
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
