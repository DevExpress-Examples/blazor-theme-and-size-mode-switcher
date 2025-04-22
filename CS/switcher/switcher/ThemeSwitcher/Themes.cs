using DevExpress.Blazor;

namespace switcher.ThemeSwitcher;

struct BootstrapThemeNames {
    public const string Cerulean = "cerulean";
    public const string Cyborg = "cyborg";
    public const string Default = "default";
    public const string DefaultDark = "default-dark";
    public const string Flatly = "flatly";
    public const string Journal = "journal";
    public const string Litera = "litera";
    public const string Lumen = "lumen";
    public const string Lux = "lux";
    public const string Pulse = "pulse";
    public const string Simplex = "simplex";
    public const string Solar = "solar";
    public const string Superhero = "superhero";
    public const string United = "united";
    public const string Yeti = "yeti";
}

public static class BlazorThemes {
    const string HighlightJsAndroidTheme = "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.15.6/styles/androidstudio.min.css";
    const string HighlightJsDefaultTheme = "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.15.6/styles/default.min.css";

    const string DxCommonStylesPath = "switcher-resources/css/themes/dx/common.css";
    const string FluentCommonStylesPath = "switcher-resources/css/themes/fluent/common.css";

    private static string GetBootstrapThemePath(string themeName) {
        return $"switcher-resources/css/themes/{themeName}/bootstrap.min.css";
    }

    private static string GetBootstrapFluentThemePath(string themeName) {
        return $"switcher-resources/css/themes/fluent/{themeName}.bs5.min.css";
    }

    public static void AddDefaultDxTheme(ThemeProperties properties) {
        properties.AddFilePaths(
            DxCommonStylesPath,
            HighlightJsDefaultTheme
        );
    }
    public static void AddAndroidDxTheme(ThemeProperties properties) {
        properties.AddFilePaths(
            DxCommonStylesPath,
            HighlightJsAndroidTheme
        );
    }

    public static readonly ITheme BlazingBerry = DevExpress.Blazor.Themes.BlazingBerry.Clone(AddDefaultDxTheme);
    public static readonly ITheme BlazingDark = DevExpress.Blazor.Themes.BlazingDark.Clone(AddAndroidDxTheme);
    public static readonly ITheme Purple = DevExpress.Blazor.Themes.Purple.Clone(AddDefaultDxTheme);
    public static readonly ITheme OfficeWhite = DevExpress.Blazor.Themes.OfficeWhite.Clone(AddDefaultDxTheme);

    public static readonly ITheme FluentLightBlue = DevExpress.Blazor.Themes.Fluent.Clone(properties => {
        properties.Name = "FluentLightBlue";

        properties.AddFilePaths(
            FluentCommonStylesPath,
            GetBootstrapFluentThemePath("fluent-light"),
            HighlightJsDefaultTheme
        );
    });

    public static readonly ITheme FluentDarkBlue = DevExpress.Blazor.Themes.Fluent.Clone(properties => {
        properties.Name = "FluentDarkBlue";

        properties.AddFilePaths(
            FluentCommonStylesPath,
            GetBootstrapFluentThemePath("fluent-dark"),
            HighlightJsAndroidTheme
        );

        properties.Mode = ThemeMode.Dark;
    });

    public static readonly ITheme FluentLightPurple = DevExpress.Blazor.Themes.Fluent.Clone(properties => {
        properties.Name = "FluentLightPurple";

        properties.AddFilePaths(
            FluentCommonStylesPath,
            GetBootstrapFluentThemePath("fluent-light"),
            HighlightJsDefaultTheme
        );

        properties.Palette = ThemeFluentPalette.Purple;
    });

    public static readonly ITheme FluentDarkPurple = DevExpress.Blazor.Themes.Fluent.Clone(properties => {
        properties.Name = "FluentDarkPurple";

        properties.AddFilePaths(
        FluentCommonStylesPath,
        GetBootstrapFluentThemePath("fluent-dark"),
        HighlightJsAndroidTheme
        );

        properties.Palette = ThemeFluentPalette.Purple;
        properties.Mode = ThemeMode.Dark;
    });

    public static readonly ITheme Cyborg = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Cyborg;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Cyborg));
        AddAndroidDxTheme(p);
    });

    public static readonly ITheme Cerulean = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Cerulean;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Cerulean));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme Default = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Default;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Default));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme DefaultDark = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.DefaultDark;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Default));
        AddAndroidDxTheme(p);
    });

    public static readonly ITheme Flatly = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Flatly;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Flatly));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme Journal = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Journal;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Journal));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme Litera = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Litera;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Litera));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme Lumen = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Lumen;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Lumen));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme Lux = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Lux;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Lux));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme Pulse = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Pulse;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Pulse));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme Simplex = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Simplex;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Simplex));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme Solar = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Solar;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Solar));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme Superhero = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Superhero;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Superhero));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme United = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.United;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.United));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme Yeti = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Yeti;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Yeti));
        AddDefaultDxTheme(p);
    });
}

public static class Themes {
    public static readonly Theme BlazingBerry = new Theme("blazing-berry", BlazorThemes.BlazingBerry);
    public static readonly Theme BlazingDark = new Theme("blazing-dark", BlazorThemes.BlazingDark);
    public static readonly Theme Purple = new Theme("purple", BlazorThemes.Purple);
    public static readonly Theme OfficeWhite = new Theme("office-white", BlazorThemes.OfficeWhite);
    public static readonly Theme FluentLightBlue = new Theme("light-blue", BlazorThemes.FluentLightBlue);
    public static readonly Theme FluentDarkBlue = new Theme("dark-blue", BlazorThemes.FluentDarkBlue);
    public static readonly Theme FluentLightPurple = new Theme("light-purple", BlazorThemes.FluentLightPurple);
    public static readonly Theme FluentDarkPurple = new Theme("dark-purple", BlazorThemes.FluentDarkPurple);

    public static readonly Theme Cyborg = new Theme(BootstrapThemeNames.Cyborg, BlazorThemes.Cyborg) { IsBootstrapNative = true };
    public static readonly Theme Cerulean = new Theme(BootstrapThemeNames.Cerulean, BlazorThemes.Cerulean) { IsBootstrapNative = true };
    public static readonly Theme Default = new Theme(BootstrapThemeNames.Default, BlazorThemes.Default) { IsBootstrapNative = true };
    public static readonly Theme DefaultDark = new Theme(BootstrapThemeNames.DefaultDark, BlazorThemes.DefaultDark) { BootstrapThemeMode = "dark", IsBootstrapNative = true };
    public static readonly Theme Flatly = new Theme(BootstrapThemeNames.Flatly, BlazorThemes.Flatly) { IsBootstrapNative = true };
    public static readonly Theme Journal = new Theme(BootstrapThemeNames.Journal, BlazorThemes.Journal) { IsBootstrapNative = true };
    public static readonly Theme Litera = new Theme(BootstrapThemeNames.Litera, BlazorThemes.Litera) { IsBootstrapNative = true };
    public static readonly Theme Lumen = new Theme(BootstrapThemeNames.Lumen, BlazorThemes.Lumen) { IsBootstrapNative = true };
    public static readonly Theme Lux = new Theme(BootstrapThemeNames.Lux, BlazorThemes.Lux) { IsBootstrapNative = true };
    public static readonly Theme Pulse = new Theme(BootstrapThemeNames.Pulse, BlazorThemes.Pulse) { IsBootstrapNative = true };
    public static readonly Theme Simplex = new Theme(BootstrapThemeNames.Simplex, BlazorThemes.Simplex) { IsBootstrapNative = true };
    public static readonly Theme Solar = new Theme(BootstrapThemeNames.Solar, BlazorThemes.Solar) { IsBootstrapNative = true };
    public static readonly Theme Superhero = new Theme(BootstrapThemeNames.Superhero, BlazorThemes.Superhero) { IsBootstrapNative = true };
    public static readonly Theme United = new Theme(BootstrapThemeNames.United, BlazorThemes.United) { IsBootstrapNative = true };
    public static readonly Theme Yeti = new Theme(BootstrapThemeNames.Yeti, BlazorThemes.Yeti) { IsBootstrapNative = true };
}
