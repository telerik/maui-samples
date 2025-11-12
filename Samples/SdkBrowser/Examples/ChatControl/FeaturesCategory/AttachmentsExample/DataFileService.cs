using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsExample;

// >> chat-data-server
public static class DataFileService
{
	internal static List<PredefinedFile> predefinedFiles;

	private static readonly Dictionary<Guid, ServerFile> files = new Dictionary<Guid, ServerFile>();

	internal static async Task Init()
	{
		if (predefinedFiles != null)
		{
			return;
		}

		List<PredefinedFile> list = new List<PredefinedFile>();

		List<string> fileNames = new List<string>
		{
			"PdfDocument.pdf",
			"Presentation.pptx",
			"Accounting.xlsx",
			"Audio.mp3",
			"Video.mp4",
			"PdfDocument-Signed.pdf",
			"Archive.zip",
			"TextFile.txt",
		};

		foreach (string fileName in fileNames)
		{
			using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes($"this is fake content for file {fileName}")))
			{
				Guid guid = await UploadFile(stream);
				list.Add(new PredefinedFile { fileName = fileName, fileSize = stream.Length, guid = guid, });
			}
		}

		predefinedFiles = list;
	}

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

	internal class PredefinedFile
	{
		internal string fileName;
		internal long fileSize;
		internal Guid guid;
	}
}

// << chat-data-server