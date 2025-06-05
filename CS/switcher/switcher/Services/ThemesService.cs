using System.Text.Json;
using DevExpress.Blazor;

namespace switcher.Services;

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

public class DxThemesService {
    public const string ThemeCookieKey = "DXBZCurrentTheme";
    public const string ThemeStateCookieKey = $"{ThemeCookieKey}_Opts";
    public Theme ActiveTheme { get; private set; } = switcher.Services.Themes.FluentBlue;
    public Theme DefaultTheme => switcher.Services.Themes.FluentBlue;
    public ThemeState ThemeState { get; private set; }
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
        switcher.Services.Themes.FluentBlue,
        switcher.Services.Themes.FluentCoolBlue,
        switcher.Services.Themes.FluentDesert,
        switcher.Services.Themes.FluentMint,
        switcher.Services.Themes.FluentMoss,
        switcher.Services.Themes.FluentOrchid,
        switcher.Services.Themes.FluentPurple,
        switcher.Services.Themes.FluentRose,
        switcher.Services.Themes.FluentRust,
        switcher.Services.Themes.FluentSteel,
        switcher.Services.Themes.FluentStorm,
    ];

    public Theme CustomFluentTheme = new("CustomFluent", String.Empty) { IsFluent = true };

    public List<Theme> ClassicThemes =
    [
        switcher.Services.Themes.BlazingBerry,
        switcher.Services.Themes.BlazingDark,
        switcher.Services.Themes.Purple,
        switcher.Services.Themes.OfficeWhite
    ];

    public List<Theme> BootstrapThemes =
    [
        switcher.Services.Themes.BootstrapDefault,
        switcher.Services.Themes.BootstrapDefaultDark,
        switcher.Services.Themes.BootstrapCerulean,
        switcher.Services.Themes.BootstrapFlatly,
        switcher.Services.Themes.BootstrapJournal,
        switcher.Services.Themes.BootstrapLumen
    ];

    public List<Theme> UserDefinedThemes =
    [
        switcher.Services.Themes.BootstrapUserDefined
    ];

    public List<Theme> Themes =>
        FluentThemes
            .Concat(ClassicThemes)
            .Concat(BootstrapThemes)
            .Concat(UserDefinedThemes)
            .ToList();
}
