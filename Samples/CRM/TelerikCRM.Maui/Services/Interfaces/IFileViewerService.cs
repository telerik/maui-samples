namespace TelerikCRM.Maui.Services;

public interface IFileViewerService
{
    Task<bool> View(Stream stream, string filename);
}