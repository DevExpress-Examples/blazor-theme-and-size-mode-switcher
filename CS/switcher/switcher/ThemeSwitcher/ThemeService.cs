using System.Collections.Concurrent;

namespace switcher.ThemeSwitcher {
    public interface IThemeChangeRequestDispatcher {
        void RequestThemeChange(Theme theme);
    }

    public interface IThemeLoadNotifier {
        Task NotifyThemeLoadedAsync(Theme theme);
    }

    public class ThemeService {
        private Theme _activeTheme;
        public const string ThemeCookieKey = "DXBZCurrentTheme";
        public IThemeChangeRequestDispatcher ThemeChangeRequestDispatcher { get; set; }

        public IThemeLoadNotifier ThemeLoadNotifier { get; set; }

        public ThemeService() {
            ResourcesReadyState = new ConcurrentDictionary<string, TaskCompletionSource<bool>>();
            ThemeSets = CreateSets();
        }

        public ConcurrentDictionary<string, TaskCompletionSource<bool>> ResourcesReadyState { get; }
        public List<ThemeSet> ThemeSets { get; }
        public Theme ActiveTheme => _activeTheme;
        public Theme DefaultTheme => Themes.BlazingBerry;

        public void SetActiveThemeByName(string themeName) {
            var theme = FindThemeByName(themeName);
            if(theme != null)
                _activeTheme = theme;
            else
                _activeTheme = DefaultTheme;
        }

        private Theme FindThemeByName(string themeName) {
            var themes = ThemeSets.SelectMany(ts => ts.Themes);
            foreach(var theme in themes) {
                if(theme.Name == themeName)
                    return theme;
            }
            return null;
        }

        public class ThemeSet {
            public string Title { get; }
            public Theme[] Themes { get; }

            public ThemeSet(string title, params Theme[] themes) {
                Title = title;
                Themes = themes;
            }
        }

        private static List<ThemeSet> CreateSets() {
            return [
                new ThemeSet("DevExpress Themes", Themes.BlazingBerry, Themes.BlazingDark,
                    Themes.Purple, Themes.OfficeWhite),

                new ThemeSet("Bootstrap Themes", Themes.Default, Themes.DefaultDark, Themes.Cerulean,
                    Themes.Cyborg, Themes.Flatly,
                    Themes.Journal, Themes.Litera, Themes.Lumen, Themes.Lux, Themes.Pulse,
                    Themes.Simplex, Themes.Solar, Themes.Superhero, Themes.United, Themes.Yeti),

                new ThemeSet("Fluent Themes", Themes.FluentLightBlue, Themes.FluentDarkBlue, Themes.FluentLightPurple, Themes.FluentDarkPurple)
            ];
        }
    }
}
