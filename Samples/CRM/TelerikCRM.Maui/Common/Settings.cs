namespace TelerikCRM.Maui.Common;

public static class Settings
{
    public static bool IsFirstRun
    {
        get => Preferences.Get(nameof(IsFirstRun), true);
        set => Preferences.Set(nameof(IsFirstRun), value);
    }
}