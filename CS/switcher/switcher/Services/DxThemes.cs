using System.Globalization;
using DevExpress.Blazor;

namespace switcher.Services;

public interface IThemeChangeRequestDispatcher {
    void RequestThemeChange(Theme theme);
}

public interface IThemeLoadNotifier {
    Task NotifyThemeLoadedAsync(Theme theme);
}

public static class BlazorThemes {
    public static readonly ITheme FluentLight = Themes.Fluent.Clone(properties => {
        properties.AddFilePaths($"css/site-fluent.css", $"switcher-resources/css/fluent-light.min.css");
    });

    public static readonly ITheme FluentDark = Themes.Fluent.Clone(properties => {
        properties.Mode = ThemeMode.Dark;
        properties.AddFilePaths($"css/site-fluent.css", $"switcher-resources/css/fluent-dark.min.css");
    });

    public static readonly ITheme BlazingBerry = Themes.BlazingBerry;
    public static readonly ITheme BlazingDark = Themes.BlazingDark;
    public static readonly ITheme Purple = Themes.Purple;
    public static readonly ITheme OfficeWhite = Themes.OfficeWhite;

    public static readonly ITheme Bootstrap = Themes.BootstrapExternal.Clone(properties => {
        properties.Name = "bootstrap";
        properties.AddFilePaths($"css/bootstrap/bootstrap.min.css");
    });

    public static readonly ITheme BootstrapDark = Themes.BootstrapExternal.Clone(properties => {
        properties.Name = "bootstrap-dark";
        properties.AddFilePaths($"css/bootstrap/bootstrap.min.css");
    });
}

public class Theme(string name, ITheme theme) {
    public string Name { get; } = name;
    public string Title { get { return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Name.Replace("-", " ")); } }
    public string IconCssClass { get { return Name.ToLower(); } }
    public bool IsBootstrapNative { get; set; }
    public string BootstrapThemeMode { get; set; } = "light";
    public string GetCssClass(bool isActive) => isActive ? "active" : "text-body";
    public ITheme CurrentTheme { get; set; } = theme;
}

public static class DxThemes {
    public static readonly Theme FluentLight = new("fluent-light", BlazorThemes.FluentLight);
    public static readonly Theme FluentDark = new("fluent-dark", BlazorThemes.FluentDark);
    public static readonly Theme BlazingBerry = new("blazing-berry", BlazorThemes.BlazingBerry);
    public static readonly Theme BlazingDark = new("blazing-dark", BlazorThemes.BlazingDark);
    public static readonly Theme Purple = new("purple", BlazorThemes.Purple);
    public static readonly Theme OfficeWhite = new("office-white", BlazorThemes.OfficeWhite);
    public static readonly Theme Bootstrap = new("default", BlazorThemes.Bootstrap) { IsBootstrapNative = true };
    public static readonly Theme BootstrapDark = new("default-dark", BlazorThemes.BootstrapDark) { IsBootstrapNative = true, BootstrapThemeMode = "dark"};
}

public class DxThemesService {
    public const string ThemeCookieKey = "DXBZCurrentTheme";
    public List<ThemeSet> ThemeSets { get; } = CreateSets();
    public Theme ActiveTheme { get; private set; } = DxThemes.BlazingBerry;
    public Theme DefaultTheme => DxThemes.BlazingBerry;
    public bool IsFluentActive => ActiveTheme == DxThemes.FluentLight || ActiveTheme == DxThemes.FluentDark;
    public bool IsBootstrapDarkActive => ActiveTheme == DxThemes.BootstrapDark;
    public bool IsFluentDarkModeActive => ActiveTheme == DxThemes.FluentDark;
    public bool IsActiveThemeDark => IsBootstrapDarkActive || IsFluentDarkModeActive;
    public IThemeLoadNotifier ThemeLoadNotifier { get; set; }
    public IThemeChangeRequestDispatcher ThemeChangeRequestDispatcher { get; set; }

    public void SetActiveThemeByName(string themeName) {
        var theme = FindThemeByName(themeName);
        if(theme != null)
            ActiveTheme = theme;
        else
            ActiveTheme = DefaultTheme;
    }

    private Theme? FindThemeByName(string themeName) {
        var themes = ThemeSets.SelectMany(ts => ts.Themes);
        foreach (var theme in themes){
            if(theme.Name == themeName)
                return theme;
        }
        return null;
    }

    public class ThemeSet(string title, params Theme[] themes) {
        public string Title { get; } = title;
        public Theme[] Themes { get; } = themes;
    }

    private static List<ThemeSet> CreateSets() {
        return
        [
            new ThemeSet("DevExpress Themes", DxThemes.BlazingBerry, DxThemes.BlazingDark, DxThemes.Purple, DxThemes.OfficeWhite),
            new ThemeSet("Bootstrap Themes", DxThemes.Bootstrap, DxThemes.BootstrapDark),
            new ThemeSet("Fluent Themes", DxThemes.FluentLight, DxThemes.FluentDark)
        ];
    }
}
