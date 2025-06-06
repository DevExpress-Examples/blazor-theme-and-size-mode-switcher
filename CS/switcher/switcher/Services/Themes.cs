using DevExpress.Blazor;

namespace switcher.Services;

public class Theme {
    public string Title { get; }
    public string Name { get; }
    public string MenuBackgroundColor { get; }
    private ThemeFluentAccentColor? FluentAccentColor { get; }

    public Theme(string name, string title) {
        Name = name;
        Title = title;
    }

    public Theme(string name, string title, string menuBackgroundColor, ThemeFluentAccentColor color)
        : this(name, title) {
        FluentAccentColor = color;
        MenuBackgroundColor = menuBackgroundColor;
    }

    public ITheme ApplyStoredState(ThemeState themeState) {
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
}
