namespace TelerikCRM.Maui.Services;

public interface IAlertService
{
    Task DisplayAlertAsync(string title, string message, string cancel);

    Task DisplayReadOnlyAlertAsync();
}
