using DevExpress.Blazor;

namespace switcher.Services;

public enum MyTheme {
    FluentLight,
    FluentDark,

    BlazingBerry,
    BlazingDark,
    Purple,
    OfficeWhite,

    Bootstrap
}
public static class ThemesCollection {
    public static ITheme FluentLight(string? accent = null) { 
        return Themes.Fluent.Clone(props => {
            props.Name = "FluentLight" + accent?.PadLeft(8);
            props.SetCustomAccentColor(accent);
            props.AddFilePaths("css/theme-fluent.css");
        });
    }
    public static ITheme FluentDark(string? accent = null) {
        return Themes.Fluent.Clone(props => {
            props.Name = "FluentDark" + accent?.PadLeft(8);
            props.SetCustomAccentColor(accent);
            props.Mode = ThemeMode.Dark;
            props.AddFilePaths("css/theme-fluent.css");
        });
    }

    public static readonly ITheme BlazingBerry = Themes.BlazingBerry.Clone(props => {
        props.AddFilePaths("css/theme-bs.css");
    });
    public static readonly ITheme BlazingDark = Themes.BlazingDark.Clone(props => {
        props.AddFilePaths("css/theme-bs.css");
    });
    public static readonly ITheme Purple = Themes.Purple.Clone(props => {
        props.AddFilePaths("css/theme-bs.css");
    });
    public static readonly ITheme OfficeWhite = Themes.OfficeWhite.Clone(props => {
        props.AddFilePaths("css/theme-bs.css");
    });

    public static readonly ITheme BootstrapDefault = Themes.BootstrapExternal.Clone(props => {
        props.Name = "Bootstrap";
        props.AddFilePaths("https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css");
        props.AddFilePaths("css/theme-bs.css");
    });
}