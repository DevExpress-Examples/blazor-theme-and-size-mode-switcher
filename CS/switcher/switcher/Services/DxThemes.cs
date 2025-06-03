using System.Globalization;
using DevExpress.Blazor;

namespace switcher.Services;

public interface IThemeChangeRequestDispatcher {
    void RequestThemeChange(Theme theme);
}

public interface IThemeLoadNotifier {
    Task NotifyThemeLoadedAsync(Theme theme);
}

public class Theme : ITheme {
    private ITheme _theme;
    public Theme(ITheme theme) {
        _theme = theme;
    }
    public List<string> GetFilePaths() {
        var paths = _theme.GetFilePaths();
        if(IsFluent)
            paths.AddRange(["css/site-fluent.css", $"switcher-resources/css/fluent-{(IsDarkMode ? "dark" : "light")}.min.css"]);
        if(IsBootstrapNative){
            if(!Name.StartsWith("default")){
                paths.Add($"css/bootstrap/{Name}/bootstrap.min.css");
            }
            paths.Add("css/bootstrap/bootstrap.min.css");
        }
        return paths;
    }
    public string Name { get; init; }
    public string Title => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Name.Replace("-", " "));
    public bool IsDarkMode { get; set; }
    public bool IsFluent { get; set; }
    public bool IsBootstrapNative { get; set; }
}

public static class DxThemes {
    public static readonly Theme BlazingBerry = new(Themes.BlazingBerry) { Name = "blazing-berry" };
    public static readonly Theme BlazingDark = new(Themes.BlazingDark) { Name = "blazing-dark" };
    public static readonly Theme Purple = new(Themes.Purple) { Name = "purple" };
    public static readonly Theme OfficeWhite = new(Themes.OfficeWhite) { Name = "office-white" };
    public static readonly Theme Bootstrap = new(Themes.BootstrapExternal) { Name = "default", IsBootstrapNative = true };
    public static readonly Theme BootstrapDark = new(Themes.BootstrapExternal) { Name = "default-dark", IsBootstrapNative = true, IsDarkMode = true };
    public static readonly Theme FluentLight = new(Themes.Fluent) { Name = "fluent-light", IsFluent = true};
    public static readonly Theme FluentDark = new(Themes.Fluent.Clone(x => x.Mode = ThemeMode.Dark)) { Name = "fluent-dark", IsFluent = true, IsDarkMode = true };
    public static readonly Theme MyTheme = new(Themes.BootstrapExternal) { Name = "my-theme", IsBootstrapNative = true };
}

public class DxThemesService {
    public const string ThemeCookieKey = "DXBZCurrentTheme";
    public List<ThemeSet> ThemeSets { get; } = CreateSets();
    public Theme ActiveTheme { get; private set; } = DxThemes.BlazingBerry;
    public Theme DefaultTheme => DxThemes.BlazingBerry;
    public bool IsActiveThemeDark => ActiveTheme.IsDarkMode;
    public bool IsFluentActive => ActiveTheme.IsFluent;
    public bool IsBootstrapDarkActive => ActiveTheme.IsBootstrapNative && IsActiveThemeDark;
    public bool IsFluentDarkModeActive => IsFluentActive && IsActiveThemeDark;
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
            new ThemeSet("Bootstrap Themes", DxThemes.Bootstrap, DxThemes.BootstrapDark, DxThemes.MyTheme),
            new ThemeSet("Fluent Themes", DxThemes.FluentLight, DxThemes.FluentDark)
        ];
    }
}
