using System.Collections.Concurrent;

namespace TelerikCRM.Maui.Services;

internal static class ImageCache
{
    // TODO: Implement cache size limit and eviction policy (e.g., LRU) to prevent unbounded memory growth.
    // The current implementation caches all downloaded images indefinitely, which could lead to memory
    // issues in production scenarios with many unique image URLs.
    private static readonly ConcurrentDictionary<string, byte[]> cache = new();
    private static readonly HttpClient httpClient = new();

    internal static ImageSource GetImageSource(string uri)
    {
        if (string.IsNullOrEmpty(uri))
        {
            return ImageSource.FromFile("art_placeholder.png");
        }

        if (!Uri.TryCreate(uri, UriKind.Absolute, out _))
        {
            return ImageSource.FromFile(uri);
        }

        if (cache.TryGetValue(uri, out var cachedBytes))
        {
            return ImageSource.FromStream(() => new MemoryStream(cachedBytes));
        }

        return new StreamImageSource
        {
            Stream = async (cancellationToken) =>
            {
                if (cache.TryGetValue(uri, out var bytes))
                {
                    return new MemoryStream(bytes);
                }

                try
                {
                    bytes = await httpClient.GetByteArrayAsync(uri, cancellationToken);
                    cache.TryAdd(uri, bytes);
                    return new MemoryStream(bytes);
                }
                catch (Exception) when (true)
                {
                    // Network error, timeout, cancellation, or other unexpected errors.
                    // Return null so MAUI falls back to the FallbackValue/TargetNullValue in the binding.
                    return null;
                }
            }
        };
    }

    internal static void Invalidate(string uri)
    {
        if (!string.IsNullOrEmpty(uri))
        {
            cache.TryRemove(uri, out _);
        }
    }
}
