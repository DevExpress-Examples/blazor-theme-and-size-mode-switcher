using DevExpress.Blazor;

namespace switcher.Services;

public class Theme : ITheme {
    public ITheme DxTheme { get; }
    public string Title { get; }
    public List<string> GetFilePaths() => DxTheme.GetFilePaths();
    public string Name { get; init; }
    public string MenuBackgroundColor { get; }
    public bool IsFluent { get; set; }
    public bool IsBootstrapNative { get; set; }
    public string BootstrapThemeMode { get; init; } = "light";
    private ThemeFluentAccentColor? FluentAccentColor { get; }
    public static string? GetCssClass(bool isActive) => isActive ? "active" : null;

    public Theme(string name, string title) {
        Name = name;
        Title = title;
    }

    public Theme(DxTheme? theme, string name, string title, string menuBackgroundColor, string? cssDirectoryName) : this(name, title) {
        DxTheme = theme ?? DevExpress.Blazor.Themes.BootstrapExternal.Clone(p => {
            p.Name = name;
            p.AddFilePaths("css/theme-bs.css", $"switcher-resources/themes/{cssDirectoryName ?? name}/bootstrap.min.css");
        });
        MenuBackgroundColor = menuBackgroundColor;
    }

    public Theme(DxTheme theme, string name, string title, string menuBackgroundColor)
        : this(theme, name, title, menuBackgroundColor, null) {
    }
    public Theme(string name, string title, string menuBackgroundColor)
        : this(null, name, title, menuBackgroundColor, null) {
        IsBootstrapNative = true;
    }
    public Theme(string name, string title, string menuBackgroundColor, string cssDirectoryName)
        : this(null, name, title, menuBackgroundColor, cssDirectoryName) {
    }

    public Theme(string name, string title, string menuBackgroundColor, ThemeFluentAccentColor color)
        : this(name, title) {
        FluentAccentColor = color;
        IsFluent = true;
        MenuBackgroundColor = menuBackgroundColor;
    }

    public ITheme ApplyStoredState(ThemeState themeState) {
        if(!IsFluent)
            return DxTheme;

        return DevExpress.Blazor.Themes.Fluent.Clone(properties => {
            properties.Mode = themeState.Mode ?? ThemeMode.Light;

            if(FluentAccentColor != null)
                properties.AccentColor = FluentAccentColor.Value;

            if(themeState.CustomAccentColor != null)
                properties.SetCustomAccentColor(themeState.CustomAccentColor);

            properties.Name = $"{Name}{properties.Mode}{themeState.CustomAccentColor}";
            properties.AddFilePaths("css/theme-fluent.css");
        });
    }
}

public static class Themes {
    public static readonly Theme BlazingBerry = new(DevExpress.Blazor.Themes.BlazingBerry, "blazing-berry", "Blazing Berry", "#5c2d91");
    public static readonly Theme BlazingDark = new(DevExpress.Blazor.Themes.BlazingDark, "blazing-dark", "Blazing Dark", "#46444a");
    public static readonly Theme Purple = new(DevExpress.Blazor.Themes.Purple, "purple", "Purple", "#7989ff");
    public static readonly Theme OfficeWhite = new(DevExpress.Blazor.Themes.OfficeWhite, "office-white", "Office White", "#fe7109");

    public static readonly Theme FluentBlue = new("fluent-blue", "Blue", "#0f6cbd", ThemeFluentAccentColor.Blue);
    public static readonly Theme FluentCoolBlue = new("fluent-cool-blue", "Cool Blue", "#2d7d9a", ThemeFluentAccentColor.CoolBlue);
    public static readonly Theme FluentDesert = new("fluent-desert", "Desert", "#847545", ThemeFluentAccentColor.Desert);
    public static readonly Theme FluentMint = new("fluent-mint", "Mint", "#018574", ThemeFluentAccentColor.Mint);
    public static readonly Theme FluentMoss = new("fluent-moss", "Moss", "#486860", ThemeFluentAccentColor.Moss);
    public static readonly Theme FluentOrchid = new("fluent-orchid", "Orchid", "#c239b3", ThemeFluentAccentColor.Orchid);
    public static readonly Theme FluentPurple = new("fluent-purple", "Purple", "#5b5fc7", ThemeFluentAccentColor.Purple);
    public static readonly Theme FluentRose = new("fluent-rose", "Rose", "#ea005e", ThemeFluentAccentColor.Rose);
    public static readonly Theme FluentRust = new("fluent-rust", "Rust", "#da3b01", ThemeFluentAccentColor.Rust);
    public static readonly Theme FluentSteel = new("fluent-steel", "Steel", "#68768a", ThemeFluentAccentColor.Steel);
    public static readonly Theme FluentStorm = new("fluent-storm", "Storm", "#4c4a48", ThemeFluentAccentColor.Storm);

    public static readonly Theme BootstrapDefault = new("default", "Default", "#027BFF");
    public static readonly Theme BootstrapDefaultDark = new("default-dark", "Default Dark", "#212529", "default") { BootstrapThemeMode = "dark" };
    public static readonly Theme BootstrapCerulean = new("cerulean", "Cerulean", "#2EA4E7");
    public static readonly Theme BootstrapFlatly = new("flatly", "Flatly", "#DBE4EC");
    public static readonly Theme BootstrapJournal = new("journal", "Journal", "#EB6864");
    public static readonly Theme BootstrapLumen = new("lumen", "Lumen", "#158CBA");

    // user defined
    public static readonly Theme BootstrapUserDefined = new("user-defined", "User Defined", "red");
}
