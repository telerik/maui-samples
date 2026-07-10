namespace TelerikCRM.Maui.Common;

public static class Extensions
{
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> data)
    {
        var items = new List<T>();
        await foreach (var item in data)
        {
            items.Add(item);
        }

#pragma warning disable CsWinRT1030
        return items;
#pragma warning restore CsWinRT1030
    }

    public static ImageFormat DetermineImageFormat(this string path)
    {
        try
        {
            var ext = Path.GetExtension(path);
            Enum.TryParse(ext, true, out ImageFormat val);

            return val;
        }
        catch (Exception)
        {
            return ImageFormat.Png;
        }
    }

    // TODO: Delete this once ImageEditor is replaced with file picker.
    public static async Task<string> SaveToLocalFolderAsync(this Stream dataStream, string fileName)
    {
        var localFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        // Use Combine so that the correct file path slashes are used
        var filePath = Path.Combine(localFolder, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        await using var fileStream = File.OpenWrite(filePath);
        if (dataStream.CanSeek)
        {
            dataStream.Position = 0;
        }

        await dataStream.CopyToAsync(fileStream);
        return filePath;
    }
}