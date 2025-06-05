using System.Text.Json;
using DevExpress.Blazor;
using DevExpress.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using switcher.Services;

public class ThemeJsChangeDispatcher : ComponentBase, IThemeChangeRequestDispatcher, IAsyncDisposable {
    [Parameter] public string InitialThemeName { get; set; }
    [Parameter] public ThemeState InitialThemeState { get; set; }

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
        if(ThemesService.ThemeState == null)
            ThemesService.SetThemeState(InitialThemeState);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender) {
        await base.OnAfterRenderAsync(firstRender);

        if(firstRender)
            _module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./switcher-resources/js/theme-controller.js");
    }

    public async void RequestThemeChange(Theme theme) {
        if(_pendingTheme != null) return;
        _pendingTheme = theme;

        var state = theme.ApplyStoredState(ThemesService.ThemeState);
        await DxThemesService.SetTheme(state);
        
        await _module.InvokeVoidAsync(
        "ThemeController.switchTheme", 
        theme.BootstrapThemeMode,
        theme.Name,
        JsonSerializer.Serialize(ThemesService.ThemeState),
        switcher.Services.DxThemesService.ThemeCookieKey,
        switcher.Services.DxThemesService.ThemeStateCookieKey,
        DotNetObjectReference.Create(this));
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
