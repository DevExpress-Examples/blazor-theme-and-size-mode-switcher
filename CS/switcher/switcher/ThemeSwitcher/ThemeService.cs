namespace switcher.ThemeSwitcher {
    public interface IThemeChangeRequestDispatcher {
        void RequestThemeChange(Theme theme);
    }

    public interface IThemeLoadNotifier {
        Task NotifyThemeLoadedAsync(Theme theme);
    }

    public class ThemeService {
        public static readonly string DEFAULT_THEME_NAME = "blazing-berry";
        static readonly string[] NEW_BLAZOR_THEMES = [DEFAULT_THEME_NAME, "blazing-dark", "purple", "office-white", "fluent-light", "fluent-dark"];
        static readonly Dictionary<string, string> HIGHLIGHT_JS_THEME = new() {
            { DEFAULT_THEME_NAME, "default" },
            { "blazing-dark", "androidstudio" },
            { "cyborg", "androidstudio" },
            { "default-dark", "androidstudio" }
        };

        readonly Theme defaultTheme;
        public Theme ActiveTheme { get; private set;  }
        public List<ThemeSet> ThemeSets { get; }
        public IThemeChangeRequestDispatcher? ThemeChangeRequestDispatcher { get; set; }
        public IThemeLoadNotifier? ThemeLoadNotifier { get; set; }

        public ThemeService() {
            ThemeSets = CreateSets(this);

            ActiveTheme = defaultTheme = FindThemeByName(DEFAULT_THEME_NAME)!;
        }

        public string GetThemeCssUrl(Theme theme) {
            if (Array.IndexOf(NEW_BLAZOR_THEMES, theme.Name) > -1)
                return $"_content/DevExpress.Blazor.Themes/{theme.Name}.bs5.min.css";
            return $"_content/DevExpress.Blazor.Themes/bootstrap-external.bs5.min.css";
        }
        public string? GetBootstrapThemeCssUrl(Theme theme) {
            return theme.IsBootstrapNative ?
                $"switcher-resources/css/themes/{theme.ThemePath}/bootstrap.min.css" : null;
        }
        public string GetHighlightJSThemeCssUrl(Theme theme) {
            var highlightjsTheme = HIGHLIGHT_JS_THEME[DEFAULT_THEME_NAME];
            if(HIGHLIGHT_JS_THEME.ContainsKey(theme.Name))
                highlightjsTheme = HIGHLIGHT_JS_THEME[theme.Name];
            return $"https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.15.6/styles/{highlightjsTheme}.min.css";
        }
        public void SetActiveThemeByName(string? themeName) {
            ActiveTheme = FindThemeByName(themeName) ?? defaultTheme;
        }

        private Theme? FindThemeByName(string? themeName) {
            var themes = ThemeSets.SelectMany(ts => ts.Themes);
            foreach(var theme in themes) {
                if(theme.Name == themeName)
                    return theme;
            }
            return null;
        }

        private static List<ThemeSet> CreateSets(ThemeService config) {
            return new List<ThemeSet>() {
                new ThemeSet("DevExpress Themes", NEW_BLAZOR_THEMES),
                new ThemeSet("Bootstrap Themes", "default", "default-dark", "cerulean", "cyborg", "flatly", "journal", "litera", "lumen", "lux", "pulse", "simplex", "solar", "superhero", "united", "yeti"),
            };
        }
    }
}
