using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Maui.Controls;
using static SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsExample.DataFileService;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsExample;

// >> chat-with-attachments-view-model
public class ChatWithAttachmentsViewModel : NotifyPropertyChangedBase
{
	private int attachmentsCount;
	private ObservableCollection<AttachedFileData> attachedFiles;
	private Command sendMessageCommand;
	private object message;

	public ChatWithAttachmentsViewModel()
	{
		this.Me = "human";
		this.Bot = "bot";

		this.Items = new ObservableCollection<MessageItem>();
		this.sendMessageCommand = new Command(this.ExecuteSendMessageCommand, this.CanExecuteSendMessageCommand);
		this.AttachedFiles = new ObservableCollection<AttachedFileData>();
		this.AttachFilesCommand = new Command(this.AttachFiles);

		this.LoadDataFromService();
	}

	public object Me { get; }

	public object Bot { get; }

	public object Message 
	{ 
		get => this.message;
		set => this.UpdateValue(ref this.message, value);
	}

	public IList<MessageItem> Items { get; set; }

	public ICommand AttachFilesCommand { get; }

	public ICommand SendMessageCommand { get => this.sendMessageCommand; }

	public ObservableCollection<AttachedFileData> AttachedFiles
	{
		get => this.attachedFiles;
		set => this.UpdateValue(ref this.attachedFiles, value, this.OnAttachedFilesChanged);
	}

	private async void LoadDataFromService()
	{
		await DataFileService.Init();
		List<PredefinedFile> files = DataFileService.predefinedFiles;

		this.Items.Add(new AttachmentsItem { Author = this.Bot, Text = "Review this document and sign it, please", Attachments = this.GetAttachments(1) });
		this.Items.Add(new AttachmentsItem { Author = this.Bot, Text = "Check out these files and apply the needed changes.", Attachments = this.GetAttachments(2) });
		this.Items.Add(new AttachmentsItem { Author = this.Bot, Text = "I am sending the song audio and the video. Please let me know what you think", Attachments = this.GetAttachments(2) });
		this.Items.Add(new AttachmentsItem { Author = this.Me, Text = "Document signed!", Attachments = this.GetAttachments(1) });
		this.Items.Add(new AttachmentsItem { Author = this.Me, Text = "The files are added to the archive", Attachments = this.GetAttachments(1) });
		this.Items.Add(new AttachmentsItem { Author = this.Me, Text = "The song is really good, and the video is awesome. Congrats!", Attachments = this.GetAttachments(0) });
	}

	private void OnAttachedFilesChanged(ObservableCollection<AttachedFileData> oldValue)
	{
		if (oldValue != null)
		{
			oldValue.CollectionChanged -= this.AttachedFiles_CollectionChanged;
		}

		if (this.attachedFiles != null)
		{
			this.attachedFiles.CollectionChanged += this.AttachedFiles_CollectionChanged;
		}

		this.sendMessageCommand.ChangeCanExecute();
	}

	private async void AttachedFiles_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
	{
		this.sendMessageCommand.ChangeCanExecute();

		if (args.Action == NotifyCollectionChangedAction.Add)
		{
			// We want to start the upload process for the newly added file.
			foreach (AttachedFileData attachedFileData in args.NewItems)
			{
				await this.TryUploadFile(attachedFileData);
			}
		}
		else if (args.Action == NotifyCollectionChangedAction.Remove)
		{
			// User decided to not send the file, so delete it from the server.
			foreach (AttachedFileData attachedFileData in args.OldItems)
			{
				this.TryDeleteFile(attachedFileData);
			}
		}

		this.sendMessageCommand.ChangeCanExecute();
	}

	private async Task TryUploadFile(AttachedFileData attachedFileData)
	{
		if (!this.AttachedFiles.Contains(attachedFileData))
		{
			// The item was removed before we had a chance to upload it.
			return;
		}

		using (Stream stream = await attachedFileData.GetStream())
		{
			Guid guid = await DataFileService.UploadFile(stream);

			if (!this.AttachedFiles.Contains(attachedFileData))
			{
				// The item was removed while upload was running.
				DataFileService.DeleteFile(guid);
				return;
			}
			else if (guid != Guid.Empty)
			{
				attachedFileData.Guid = guid;
			}
		}
	}

	private void TryDeleteFile(AttachedFileData attachedFileData)
	{
		DataFileService.DeleteFile(attachedFileData.Guid);
	}

	private void AttachFiles(object commandParameter)
	{
		IList<AttachedFileData> filesToAttach = (IList<AttachedFileData>)commandParameter;
		foreach (AttachedFileData attachedFileData in filesToAttach)
		{
			this.AttachedFiles.Add(attachedFileData);
		}

		// Instruct the RadChat to not attempt to auto add files.
		filesToAttach.Clear();
	}

	private ObservableCollection<AttachmentData> GetAttachments(int count)
	{
		ObservableCollection<AttachmentData> list = new ObservableCollection<AttachmentData>();

		for (int i = 0; i < count; i++)
		{
			PredefinedFile file = DataFileService.predefinedFiles[this.attachmentsCount % DataFileService.predefinedFiles.Count];
			AttachmentData attachmentsData = new AttachmentData { Name = file.fileName, Size = file.fileSize, Guid = file.guid, };
			list.Add(attachmentsData);
			this.attachmentsCount++;
		}

		return list;
	}

	private static AttachmentData CreateAttachmentData(AttachedFileData attachedFile)
	{
		return new AttachmentData { Name = attachedFile.Name, Size = attachedFile.Size, Guid = attachedFile.Guid, };
	}

	private bool CanExecuteSendMessageCommand(object arg)
	{
		string myMessageString = this.message as string;
		if (string.IsNullOrWhiteSpace(myMessageString))
		{
			return true;
		}

		if (this.AttachedFiles.Any(file => file.Guid == Guid.Empty))
		{
			return false;
		}

		return true;
	}

	private void ExecuteSendMessageCommand(object obj)
	{
		string myMessageString = this.message as string;
		this.Message = string.Empty;
		ObservableCollection<AttachedFileData> attachedFiles = this.AttachedFiles;

		if (attachedFiles.Count != 0)
		{
			// Create a new collection without deleting the uploaded files.
			this.AttachedFiles = new ObservableCollection<AttachedFileData>();
			ObservableCollection<AttachmentData> attachments = new ObservableCollection<AttachmentData>(attachedFiles.Select(CreateAttachmentData));
			AttachmentsItem attachmentsMessage = new AttachmentsItem { Author = this.Me, Text = myMessageString, Attachments = attachments };
			this.Items.Add(attachmentsMessage);
		}
		else
		{
			MessageItem message = new MessageItem { Author = this.Me, Text = myMessageString, };
			this.Items.Add(message);
		}
	}
}
// << chat-with-attachments-view-model