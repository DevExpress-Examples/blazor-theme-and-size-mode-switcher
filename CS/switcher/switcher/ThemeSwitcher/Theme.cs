using System.Globalization;
using DevExpress.Blazor;

namespace switcher.ThemeSwitcher {
    public class Theme {
        public string Name { get; }
        public string Title { get { return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Name.Replace("-", " ")); } }
        public string IconCssClass { get { return Name.ToLower(); } }
        public bool IsFluent => CurrentTheme is DxThemeFluent;
        public bool IsBootstrapNative { get; set; }
        public string BootstrapThemeMode { get; set; } = "light";
        public string GetCssClass(bool isActive) => isActive ? "active" : "text-body";
        public ITheme CurrentTheme { get; set; }
        public Theme(string name, ITheme theme) {
            Name = name;
            CurrentTheme = theme;
        }
    }
}
