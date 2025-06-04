using System.Text.Json;
using DevExpress.Blazor;

namespace switchernew.Services;

public interface IThemeChangeRequestDispatcher {
    void RequestThemeChange(Theme theme);
}

public interface IThemeLoadNotifier {
    Task NotifyThemeLoadedAsync(Theme theme);
}

public class ThemeState {
    public ThemeMode? Mode { get; set; } = ThemeMode.Light;
    public string? CustomAccentColor { get; set; }

    public override string ToString() {
        return JsonSerializer.Serialize(this);
    }
}

// public static class ThemesService {
//     public static readonly Theme BlazingBerry = new(DevExpress.Blazor.Themes.BlazingBerry) { Name = "blazing-berry" };
//     public static readonly Theme BlazingDark = new(DevExpress.Blazor.Themes.BlazingDark) { Name = "blazing-dark" };
//     public static readonly Theme Purple = new(DevExpress.Blazor.Themes.Purple) { Name = "purple" };
//     public static readonly Theme OfficeWhite = new(DevExpress.Blazor.Themes.OfficeWhite) { Name = "office-white" };
//     public static readonly Theme Bootstrap = new(DevExpress.Blazor.Themes.BootstrapExternal) { Name = "default", IsBootstrapNative = true };
//     public static readonly Theme BootstrapDark = new(DevExpress.Blazor.Themes.BootstrapExternal) { Name = "default-dark", IsBootstrapNative = true, IsDarkMode = true };
//     public static readonly Theme FluentLight = new(DevExpress.Blazor.Themes.Fluent) { Name = "fluent-light", IsFluent = true };
//     public static readonly Theme FluentDark = new(DevExpress.Blazor.Themes.Fluent.Clone(x => x.Mode = ThemeMode.Dark)) { Name = "fluent-dark", IsFluent = true, IsDarkMode = true };
//     public static readonly Theme FluentOther = new(DevExpress.Blazor.Themes.Fluent.Clone(x => x.AccentColor = ThemeFluentAccentColor.Rose)) { Name = "fluent-rose", IsFluent = true };
//     public static readonly Theme MyTheme = new(DevExpress.Blazor.Themes.BootstrapExternal) { Name = "my-theme", IsBootstrapNative = true };
// }

public class DxThemesService {
    public const string ThemeCookieKey = "DXBZCurrentTheme";
    public const string ThemeStateCookieKey = $"{ThemeCookieKey}_Opts";
    public Theme ActiveTheme { get; private set; } = switchernew.Services.Themes.FluentBlue;
    public Theme DefaultTheme => switchernew.Services.Themes.FluentBlue;
    public ThemeState ThemeState { get; private set; }
    // public bool IsFluentActive => ActiveTheme == switchernew.Services.Themes.FluentLight || ActiveTheme == switchernew.Services.Themes.FluentDark;
    // public bool IsBootstrapDarkActive => ActiveTheme == switchernew.Services.Themes.BootstrapDark;
    // public bool IsFluentDarkModeActive => ActiveTheme == switchernew.Services.Themes.FluentDark;
    // public bool IsActiveThemeDark => IsBootstrapDarkActive || IsFluentDarkModeActive;
    public IThemeLoadNotifier ThemeLoadNotifier { get; set; }
    public IThemeChangeRequestDispatcher ThemeChangeRequestDispatcher { get; set; }

    public void SetActiveThemeByName(string? themeName) {
        var theme = FindThemeByName(themeName);
        if(theme != null)
            ActiveTheme = theme;
        else
            ActiveTheme = DefaultTheme;
    }

    private Theme? FindThemeByName(string? themeName) {
        return Themes.SingleOrDefault(theme => theme.Name == themeName);
    }
    
    public void SetThemeState(ThemeState themeState) {
        ThemeState = themeState;
    }

    public List<Theme> FluentThemes =
    [
        switchernew.Services.Themes.FluentBlue,
        switchernew.Services.Themes.FluentCoolBlue,
        switchernew.Services.Themes.FluentDesert,
        switchernew.Services.Themes.FluentMint,
        switchernew.Services.Themes.FluentMoss,
        switchernew.Services.Themes.FluentOrchid,
        switchernew.Services.Themes.FluentPurple,
        switchernew.Services.Themes.FluentRose,
        switchernew.Services.Themes.FluentRust,
        switchernew.Services.Themes.FluentSteel,
        switchernew.Services.Themes.FluentStorm,
    ];

    public Theme CustomFluentTheme = new("CustomFluent", String.Empty) { IsFluent = true };

    public List<Theme> ClassicThemes =
    [
        switchernew.Services.Themes.BlazingBerry,
        switchernew.Services.Themes.BlazingDark,
        switchernew.Services.Themes.Purple,
        switchernew.Services.Themes.OfficeWhite
    ];

    public List<Theme> BootstrapThemes =
    [
        switchernew.Services.Themes.BootstrapDefault,
        switchernew.Services.Themes.BootstrapDefaultDark,
        switchernew.Services.Themes.BootstrapCerulean,
        switchernew.Services.Themes.BootstrapFlatly,
        switchernew.Services.Themes.BootstrapJournal,
        switchernew.Services.Themes.BootstrapLumen,
        new("CustomBootstrap", "Custom Bootstrap", "#5c2d91", ThemeFluentAccentColor.Blue)
    ];
    

    public List<Theme> Themes =>
        FluentThemes
            .Concat(ClassicThemes)
            .Concat(BootstrapThemes)
            .Concat([CustomFluentTheme])
            .ToList();
}
