using System.Globalization;

namespace switcher.ThemeSwitcher {
    public class Theme {
        const string BsNativeDarkModePostfix = "-dark";
        public string Name { get; }
        public string Title { get { return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Name.Replace("-", " ")); } }
        public string IconCssClass { get { return Name.ToLower(); } }
        public bool IsBootstrapNative { get; }
        public string BootstrapThemeMode => IsBootstrapNative && Name.Contains(BsNativeDarkModePostfix) ? "dark" : "light";
        public string GetCssClass(bool isActive) => isActive ? "active" : "text-body";
        public string ThemePath => IsBootstrapNative ? Name.Replace(BsNativeDarkModePostfix, string.Empty) : Name;
        public Theme(string name, bool isBootstrapNative) {
            Name = name;
            IsBootstrapNative = isBootstrapNative;
        }
    }

    public class ThemeSet {
        static readonly HashSet<string> BuiltInThemes = new HashSet<string>() {
            "blazing-berry", "blazing-dark", "purple", "office-white", "fluent-light", "fluent-dark"
        };
        public string Title { get; }
        public Theme[] Themes { get; }
        public ThemeSet(string title, params string[] themes) {
            Title = title;
            Themes = themes.Select(CreateTheme).ToArray();


            Theme CreateTheme(string name) {
                bool isBootstrapNative = !BuiltInThemes.Contains(name);
                return new Theme(name, isBootstrapNative);
            }
        }
    }
}
