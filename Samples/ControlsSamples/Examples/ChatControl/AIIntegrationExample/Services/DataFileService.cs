using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace QSF.Examples.ChatControl.AIIntegrationExample;

public static class DataFileService
{
    private static readonly Dictionary<Guid, ServerFile> files = new Dictionary<Guid, ServerFile>();

    public static async Task<Guid> UploadFile(Stream stream)
    {
        await Task.Yield();

        MemoryStream streamCopy = new MemoryStream();
        stream.CopyTo(streamCopy);
        ServerFile file = new ServerFile { stream = streamCopy };

        lock (files)
        {
            Guid guid = Guid.NewGuid();
            files[guid] = file;
            return guid;
        }
    }

    public static void DeleteFile(Guid guid)
    {
        ServerFile file;

        lock (files)
        {
            if (!files.ContainsKey(guid))
            {
                return;
            }

            file = files[guid];
            files.Remove(guid);
        }

        _ = Task.Run(() =>
        {
            lock (file)
            {
                file.stream.Dispose();
                file.isDisposed = true;
            }
        });
    }

    public static async Task<Stream> OpenFileStream(Guid guid)
    {
        await Task.Yield();

        ServerFile file;

        lock (files)
        {
            file = files[guid];
        }

        MemoryStream streamCopy = new MemoryStream();

        lock (file)
        {
            if (file.isDisposed)
            {
                throw new ObjectDisposedException("The file has been deleted from the server.");
            }

            file.stream.Position = 0;
            file.stream.CopyTo(streamCopy);
            streamCopy.Position = 0;
        }

        return streamCopy;
    }

    class ServerFile
    {
        internal MemoryStream stream;
        internal bool isDisposed;
    }
}
