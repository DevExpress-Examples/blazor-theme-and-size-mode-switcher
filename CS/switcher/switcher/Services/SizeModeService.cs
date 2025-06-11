using DevExpress.Blazor;

namespace switcher.Services;

public class SizeModeService {
    public const string SizeModeCookieKey = "DXBZSizeMode";

    public SizeMode SizeMode { get; private set; }
    public event EventHandler SizeModeChanged;

    public void Initialize(string? sizeModeString) {
        SizeMode = sizeModeString switch
        {
            "Small" => SizeMode.Small,
            "Large" => SizeMode.Large,
            _ => SizeMode.Medium,
        };
    }

    public void SetSizeMode(SizeMode sizeMode) {
        SizeMode = sizeMode;
        SizeModeChanged?.Invoke(this, EventArgs.Empty);
    }
}
