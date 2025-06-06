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
        ActiveTheme = theme ?? DefaultTheme;
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

    public Theme CustomFluentTheme = new("CustomFluent", String.Empty);

    public List<Theme> Themes => FluentThemes.Append(CustomFluentTheme).ToList();
}
