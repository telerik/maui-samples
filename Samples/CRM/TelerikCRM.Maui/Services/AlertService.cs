namespace TelerikCRM.Maui.Services;

public class AlertService : IAlertService
{
    public async Task DisplayAlertAsync(string title, string message, string cancel)
        => await Application.Current!.Windows[0].Page!.DisplayAlertAsync(title, message, cancel);

    public async Task DisplayReadOnlyAlertAsync()
        => await Application.Current!.Windows[0].Page!.DisplayAlertAsync("Read Only", "The app is currently read-only to prevent data corruption.", "OK");
}