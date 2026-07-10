namespace TelerikCRM.Maui.Services;

public class AlertService : IAlertService
{
    public async Task DisplayAlertAsync(string title, string message, string cancel)
#if NET10_0_OR_GREATER
        => await Application.Current!.Windows[0].Page!.DisplayAlertAsync(title, message, cancel);
#else
        => await Application.Current!.Windows[0].Page!.DisplayAlert(title, message, cancel);
#endif

    public async Task DisplayReadOnlyAlertAsync()
#if NET10_0_OR_GREATER
        => await Application.Current!.Windows[0].Page!.DisplayAlertAsync("Read Only", "The app is currently read-only to prevent data corruption.", "OK");
#else
        => await Application.Current!.Windows[0].Page!.DisplayAlert("Read Only", "The app is currently read-only to prevent data corruption.", "OK");
#endif
}