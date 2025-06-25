using DevExpress.Blazor;

namespace switcher.Services;

public enum MyTheme {
    Fluent_Light,
    Fluent_Dark,

    Blazing_Berry,
    Blazing_Dark,
    Purple,
    Office_White,

    Bootstrap
}
public static class ThemesCollection {
    public static readonly ITheme FluentLight = Themes.Fluent.Clone(props => {
        props.AddFilePaths("css/theme-fluent.css");
    });
    public static readonly ITheme FluentDark = Themes.Fluent.Clone(props => {
        props.Mode = ThemeMode.Dark;
        props.AddFilePaths("css/theme-fluent.css");
    });

    public static readonly ITheme BlazingBerry = Themes.BlazingBerry;
    public static readonly ITheme BlazingDark = Themes.BlazingDark;
    public static readonly ITheme Purple = Themes.Purple;
    public static readonly ITheme OfficeWhite = Themes.OfficeWhite;

    public static readonly ITheme BootstrapDefault = Themes.BootstrapExternal.Clone(props => {
        props.Name = "Bootstrap";
        props.AddFilePaths("https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css");
        props.AddFilePaths("css/theme-bs.css");
    });
}