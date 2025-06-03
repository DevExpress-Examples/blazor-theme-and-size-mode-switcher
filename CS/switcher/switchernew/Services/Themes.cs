using DevExpress.Blazor;

namespace switchernew.Services;

struct BootstrapThemeNames {
    public const string Cerulean = "cerulean";
    public const string Default = "default";
    public const string DefaultDark = "default-dark";
    public const string Flatly = "flatly";
    public const string Journal = "journal";
    public const string Lumen = "lumen";
}

public static class BlazorThemes {
    public const string HighlightJsAndroidTheme = "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.15.6/styles/androidstudio.min.css";
    public const string HighlightJsDefaultTheme = "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.15.6/styles/default.min.css";
    public const string DxCommonStylesPath = "css/dx/common.css";
    public const string FluentCommonStylesPath = "css/fluent/common.css";

    public static string GetBootstrapThemePath(string themeName) {
        return $"switcher-resources/themes/{themeName}/bootstrap.min.css";
    }

    public static string GetBootstrapFluentThemePath(string themeName) {
        return $"switcher-resources/themes/fluent/{themeName}.bs5.min.css";
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

    public static readonly ITheme Cerulean = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Cerulean;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Cerulean));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme BootstrapDefault = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Default;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Default));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme BootstrapDefaultDark = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.DefaultDark;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Default));
        AddAndroidDxTheme(p);
    });

    public static readonly ITheme BootstrapFlatly = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Flatly;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Flatly));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme BootstrapJournal = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Journal;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Journal));
        AddDefaultDxTheme(p);
    });

    public static readonly ITheme BootstrapLumen = DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
        p.Name = BootstrapThemeNames.Lumen;
        p.AddFilePaths(GetBootstrapThemePath(BootstrapThemeNames.Lumen));
        AddDefaultDxTheme(p);
    });
}

public static class Themes {
    public static readonly Theme BlazingBerry = new Theme(BlazorThemes.BlazingBerry, "blazing-berry", "Blazing Berry", "#5c2d91");

    public static readonly Theme BlazingDark = new Theme(BlazorThemes.BlazingDark, "blazing-dark", "Blazing Dark", "#46444a");

    public static readonly Theme Purple = new Theme(BlazorThemes.Purple, "purple", "Purple", "#7989ff");

    public static readonly Theme OfficeWhite = new Theme(BlazorThemes.OfficeWhite, "office-white", "Office White", "#fe7109");

    public static readonly Theme FluentBlue = new Theme("fluent-blue", "Blue", "#0f6cbd", ThemeFluentAccentColor.Blue);
    public static readonly Theme FluentCoolBlue = new Theme("fluent-cool-blue", "Cool Blue", "#2d7d9a", ThemeFluentAccentColor.CoolBlue);
    public static readonly Theme FluentDesert = new Theme("fluent-desert", "Desert", "#847545", ThemeFluentAccentColor.Desert);
    public static readonly Theme FluentMint = new Theme("fluent-mint", "Mint", "#018574", ThemeFluentAccentColor.Mint);
    public static readonly Theme FluentMoss = new Theme("fluent-moss", "Moss", "#486860", ThemeFluentAccentColor.Moss);
    public static readonly Theme FluentOrchid = new Theme("fluent-orchid", "Orchid", "#c239b3", ThemeFluentAccentColor.Orchid);
    public static readonly Theme FluentPurple = new Theme("fluent-purple", "Purple", "#5b5fc7", ThemeFluentAccentColor.Purple);
    public static readonly Theme FluentRose = new Theme("fluent-rose", "Rose", "#ea005e", ThemeFluentAccentColor.Rose);
    public static readonly Theme FluentRust = new Theme("fluent-rust", "Rust", "#da3b01", ThemeFluentAccentColor.Rust);
    public static readonly Theme FluentSteel = new Theme("fluent-steel", "Steel", "#68768a", ThemeFluentAccentColor.Steel);
    public static readonly Theme FluentStorm = new Theme("fluent-storm", "Storm", "#4c4a48", ThemeFluentAccentColor.Storm);

    public static readonly Theme BootstrapDefault =
        new Theme(BlazorThemes.BootstrapDefault, "default", "Default", "#027BFF") { IsBootstrapNative = true };

    public static readonly Theme BootstrapDefaultDark =
        new Theme(BlazorThemes.BootstrapDefaultDark, "default-dark", "Default Dark", "#212529") { BootstrapThemeMode = "dark", IsBootstrapNative = true };

    public static readonly Theme BootstrapCerulean =
        new Theme(BlazorThemes.Cerulean, "cerulean", "Cerulean", "#2EA4E7") { IsBootstrapNative = true };

    public static readonly Theme BootstrapFlatly =
        new Theme(BlazorThemes.BootstrapFlatly, "flatly", "Flatly", "#DBE4EC") { IsBootstrapNative = true };

    public static readonly Theme BootstrapJournal =
        new Theme(BlazorThemes.BootstrapJournal, "journal", "Journal", "#EB6864") { IsBootstrapNative = true };

    public static readonly Theme BootstrapLumen =
        new Theme(BlazorThemes.BootstrapLumen, "lumen", "Lumen", "#158CBA") { IsBootstrapNative = true };
}
