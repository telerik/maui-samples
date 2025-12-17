using Microsoft.Maui.Controls;
using QSF.ViewModels;
using QSF.Services;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QSF.Examples.ChatControl.FirstLookExample;

public class FirstLookViewModel : ExampleViewModel
{
    private readonly ChatService chatService;

    private bool isOperational;
    private object message;
    private Command sendMessageCommand;
    private ObservableCollection<AttachedFileData> attachedFiles;
    private bool isRecipientTyping;

    public FirstLookViewModel()
    {
        this.chatService = new ChatService();
        this.chatService.ResponseReceived += this.ChatService_ResponseReceived;
        this.chatService.IsRecipientTypingChanged += this.ChatService_IsRecipientTypingChanged;
        this.sendMessageCommand = new Command(this.ExecuteSendMessageCommand, this.CanExecuteSendMessageCommand);
        this.Me = ConversationConstants.meAuthorId;
        this.Recipient = ConversationConstants.recipientAuthorId;
        this.Messages = new ObservableCollection<MessageItem>();
        this.AttachedFiles = new ObservableCollection<AttachedFileData>();
        this.AttachFilesCommand = new Command(this.AttachFiles);
        this.RemoveAttachedFileCommand = new Command(this.RemoveAttachedFile);
    }

    public object Me { get; }
    public object Recipient { get; }
    public IList<MessageItem> Messages { get; }
    public ICommand AttachFilesCommand { get; }
    public ICommand RemoveAttachedFileCommand { get; }
    public ICommand SendMessageCommand => this.sendMessageCommand;

    public bool IsOperational
    {
        get => this.isOperational;
        set => this.UpdateValue(ref this.isOperational, value, this.OnIsOperationalChanged);
    }

    public ObservableCollection<AttachedFileData> AttachedFiles
    {
        get => this.attachedFiles;
        private set => this.UpdateValue(ref this.attachedFiles, value, this.OnAttachedFilesChanged);
    }

    public object Message
    {
        get => this.message;
        set
        {
            if (this.UpdateValue(ref this.message, value))
            {
                this.sendMessageCommand.ChangeCanExecute();
            }
        }
    }

    public bool IsRecipientTyping
    {
        get => this.isRecipientTyping;
        private set
        {
            if (this.UpdateValue(ref this.isRecipientTyping, value))
            {
                this.sendMessageCommand.ChangeCanExecute();
            }
        }
    }

    private void OnIsOperationalChanged(bool oldValue) => this.chatService.IsOperational = this.isOperational;

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
            foreach (AttachedFileData attachedFileData in args.NewItems)
            {
                await this.TryUploadFile(attachedFileData);
            }
        }
        else if (args.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (AttachedFileData attachedFileData in args.OldItems)
            {
                this.TryDeleteFile(attachedFileData);
            }
        }

        this.sendMessageCommand.ChangeCanExecute();
    }

    private void ChatService_ResponseReceived(object sender, ChatResponseEventArgs args)
    {
        MessageItem messageItem = args.Attachments != null
            ? new AttachmentsItem { Attachments = new ObservableCollection<AttachmentData>(args.Attachments) }
            : new MessageItem();

        messageItem.Author = this.Recipient;
        messageItem.Text = args.Text;
        this.Messages.Add(messageItem);
    }

    private void ChatService_IsRecipientTypingChanged(object sender, ChatRecipientTypingEventArgs args)
        => this.IsRecipientTyping = args.IsTyping;

    private async Task TryUploadFile(AttachedFileData attachedFileData)
    {
        if (!this.AttachedFiles.Contains(attachedFileData))
        {
            return;
        }

        using (Stream stream = await attachedFileData.GetStream())
        {
            Guid guid = await DataFileService.UploadFile(stream);

            if (this.AttachedFiles.Contains(attachedFileData))
            {
                attachedFileData.Guid = guid;
            }
            else
            {
                DataFileService.DeleteFile(guid);
            }
        }
    }

    private void TryDeleteFile(AttachedFileData attachedFileData)
        => DataFileService.DeleteFile(attachedFileData.Guid);

    private void AttachFiles(object commandParameter)
    {
        IList<AttachedFileData> filesToAttach = (IList<AttachedFileData>)commandParameter;
        bool hasInvalid = this.PrintFileLimitationsMessages(filesToAttach);

        if (!hasInvalid)
        {
            foreach (AttachedFileData attachedFile in filesToAttach)
            {
                this.AttachedFiles.Add(attachedFile);
            }
        }

        // Instruct the RadChat to not attempt to auto add files.
        filesToAttach.Clear();
    }

    private static void ShowToast(string message) =>
        DependencyService.Get<IToastMessageService>()?.ShortAlert(message);

    private bool PrintFileLimitationsMessages(IList<AttachedFileData> filesToAttach)
    {
        bool addedNumber = false;
        bool addedSize = false;
        int prospectiveCount = this.AttachedFiles.Count + filesToAttach.Count;

        if (ConversationConstants.maxNumberOfFiles < prospectiveCount)
        {
            addedNumber = true;
            ShowToast(string.Format(ConversationConstants.toastMaxFilesFormat, ConversationConstants.maxNumberOfFiles));
        }

        foreach (var file in filesToAttach)
        {
            if (ConversationConstants.maxFileSizeInBytes < file.Size && !addedSize)
            {
                addedSize = true;
                int mb = ConversationConstants.maxFileSizeInBytes / (1024 * 1024);
                ShowToast(string.Format(ConversationConstants.toastMaxFileSizeFormat, mb));
            }
        }

        return addedNumber || addedSize;
    }

    private void RemoveAttachedFile(object commandParameter)
    {
        if (commandParameter is int index && 0 <= index && index < this.AttachedFiles.Count)
        {
            this.AttachedFiles.RemoveAt(index);
        }
    }

    private bool CanExecuteSendMessageCommand()
    {
        var text = this.Message as string;
        
        if (string.IsNullOrWhiteSpace(text))
        {
            return false;
        }

        if (this.AttachedFiles.Any(f => f.Guid == Guid.Empty))
        {
            return false;
        }

        if (this.IsRecipientTyping)
        {
            return false;
        }

        return true;
    }

    private void ExecuteSendMessageCommand()
    {
        string userText = (this.Message as string)?.Trim();
        if (string.IsNullOrEmpty(userText))
        {
            return;
        }

        IEnumerable attachedFiles = null;
        this.Message = string.Empty;
        MessageItem messageItem;

        if (this.AttachedFiles.Count > 0)
        {
            ObservableCollection<AttachmentData> files = new ObservableCollection<AttachmentData>(this.AttachedFiles.Select(CreateAttachmentData));
            this.AttachedFiles = new ObservableCollection<AttachedFileData>();
            messageItem = new AttachmentsItem { Attachments = files };
            attachedFiles = files;
        }
        else
        {
            messageItem = new MessageItem();
        }

        messageItem.Author = this.Me;
        messageItem.Text = userText;
        this.Messages.Add(messageItem);
        this.chatService.Send(userText, attachedFiles);
    }

    private static AttachmentData CreateAttachmentData(AttachedFileData attachedFile)
        => new AttachmentData { Name = attachedFile.Name, Size = attachedFile.Size, Guid = attachedFile.Guid };
}