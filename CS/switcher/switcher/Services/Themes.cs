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
    public static readonly ITheme FluentLight = Themes.Fluent.Clone(props => {
        props.Name = "FluentLight";
        props.AddFilePaths("css/theme-fluent.css");
    });
    public static readonly ITheme FluentDark = Themes.Fluent.Clone(props => {
        props.Name = "FluentDark";
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

    public static ITheme GetTheme(string name) {
        IEnumerable<ITheme> themes = [
            FluentLight, 
            FluentDark,
            BlazingBerry,
            BlazingDark,
            Purple,
            OfficeWhite,
            BootstrapDefault
        ];
        
        return themes.SingleOrDefault(x => x.Name == name);
    }
}