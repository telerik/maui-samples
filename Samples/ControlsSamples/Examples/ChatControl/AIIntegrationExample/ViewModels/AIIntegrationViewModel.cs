using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QSF.Examples.ChatControl.AIIntegrationExample;

public class AIIntegrationViewModel : ExampleViewModel
{
    private const int maxNumberOfFiles = 2;
    private const int maxFileSizeInBytes = 2 * 1024 * 1024;
    private const string initialBotMessage = "Hello! I'm your AI assistant. How can I help you today? " + 
        "If you ask me a question, I will do my best to provide a clear and concise response. " +
        "I can also help you with analyzing up to 2 images per prompt up to 2MB each.";

    private static readonly HashSet<string> supportedFileExtensions = new HashSet<string> { ".png", ".jpg", ".jpeg" };
    private static readonly List<string> predefinedPhotos = new List<string> { "RadImageEditor.png", "SampleImage6.jpg", "SampleImage2.jpg" };
    private static int predefinedPhotosIndex = -1;

    private string chatId;
    private object prompt;
    private bool isWaitingForAIResponse;
    private Command sendMessageCommand;
    private ObservableCollection<AttachedFileData> attachedFiles;

    public AIIntegrationViewModel()
    {
        this.Me = "person";
        this.Bot = "ai_assistant";
        this.AttachFilesCommand = new Command(this.AttachFiles);
        this.RemoveAttachedFileCommand = new Command(this.RemoveAttachedFile);
        this.sendMessageCommand = new Command(this.ExecuteSendMessageCommand, this.CanExecuteSendMessageCommand);

        this.AttachedFiles = new ObservableCollection<AttachedFileData>();

        ObservableCollection<MessageItem> messages = new ObservableCollection<MessageItem>();
        messages.Add(new MessageItem { Author = this.Bot, Text = initialBotMessage });
        this.Messages = messages;

        this.SetPredefinedPrompt();
    }

    public object Me { get; }
    public object Bot { get; }
    public IList<MessageItem> Messages { get; }
    public ICommand SendMessageCommand { get => this.sendMessageCommand; }
    public ICommand AttachFilesCommand { get; }
    public ICommand RemoveAttachedFileCommand { get; }

    public ObservableCollection<AttachedFileData> AttachedFiles
    {
        get => this.attachedFiles;
        private set => this.UpdateValue(ref this.attachedFiles, value, this.OnAttachedFilesChanged);
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

    public object Prompt
    {
        get => this.prompt;
        set => this.UpdateValue(ref this.prompt, value);
    }

    public bool IsWaitingForAIResponse
    {
        get => this.isWaitingForAIResponse; 
        private set => this.UpdateValue(ref this.isWaitingForAIResponse, value, this.OnIsWaitingForAIResponseChanged);
    }

    public async void DisconnectAI()
    {
        if (!string.IsNullOrEmpty(this.chatId))
        {
            string oldValue = this.chatId;
            this.chatId = null;
            await TelerikAIService.DeleteChatId(oldValue);
        }
    }

    private static AttachmentData CreateAttachmentData(AttachedFileData attachedFile)
    {
        return new AttachmentData { Name = attachedFile.Name, Size = attachedFile.Size, Guid = attachedFile.Guid, };
    }

    private static string GetExtensionsString(HashSet<string> supportedFileExtensions)
    {
        StringBuilder builder = new StringBuilder();
        int index = 0;

        foreach (string ext in supportedFileExtensions)
        {
            if (0 < index)
            {
                if (index < supportedFileExtensions.Count - 1)
                {
                    builder.Append(", ");
                }
                else
                {
                    builder.Append(" and ");
                }
            }

            builder.Append(ext);
            index++;    
        }

        return builder.ToString();
    }

    private void SetPredefinedPrompt()
    {
        if (predefinedPhotosIndex == -1)
        {
            Random random = new Random();
            predefinedPhotosIndex = random.Next(predefinedPhotos.Count);
        }

        string photo = predefinedPhotos[predefinedPhotosIndex % predefinedPhotos.Count];
        predefinedPhotosIndex++;

        Func<Stream> getStreamFunc = () =>
        {
            Assembly assembly = typeof(AIIntegrationViewModel).Assembly;
            string file = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains(photo));
            Stream stream = assembly.GetManifestResourceStream(file);
            return stream;
        };

        long fileSize;

        using (Stream stream = getStreamFunc())
        {
            fileSize = stream.Length;
        }

        this.AttachedFiles.Add(new AttachedFileData { Name = photo, Size = fileSize, GetStream = () => Task.Run(getStreamFunc) });
        this.Prompt = "Describe my photo";
    }

    private void OnIsWaitingForAIResponseChanged(bool oldValue)
    {
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
            // The TelerikAIService does not support downloading of the files we upload to it, so in order to 
            // allow the end user to download the files they have uploaded, we upload them to the DataFileService.
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
        bool hasFilesThatDontPassRestrictions = this.PrintFileLimitationsMessages(filesToAttach);

        if (!hasFilesThatDontPassRestrictions)
        {
            foreach (AttachedFileData attachedFileData in filesToAttach)
            {
                this.AttachedFiles.Add(attachedFileData);
            }
        }

        // Instruct the RadChat to not attempt to auto add files.
        filesToAttach.Clear();
    }

    private bool PrintFileLimitationsMessages(IList<AttachedFileData> filesToAttach)
    {
        bool addedFileNumberMessage = false;
        bool addedFileSizeMessage = false;
        bool addedFileExtensionMessage = false;
        int newNumberOfFiles = this.AttachedFiles.Count + filesToAttach.Count;

        if (maxNumberOfFiles < newNumberOfFiles && !addedFileNumberMessage)
        {
            addedFileNumberMessage = true;
            this.Messages.Add(new MessageItem { Author = this.Bot, Text = $"You can attach up to {maxNumberOfFiles} files per prompt.", });
        }

        foreach (AttachedFileData attachedFileData in filesToAttach)
        {
            if (maxFileSizeInBytes < attachedFileData.Size && !addedFileSizeMessage)
            {
                addedFileSizeMessage = true;
                int maxFileSizeInMB = (int)(maxFileSizeInBytes / (1024 * 1024));
                this.Messages.Add(new MessageItem { Author = this.Bot, Text = $"The maximum file size is {maxFileSizeInMB}MB.", });
            }

            string fileExtension = Path.GetExtension(attachedFileData.Name)?.ToLowerInvariant();

            if (!supportedFileExtensions.Contains(fileExtension) && !addedFileExtensionMessage)
            {
                addedFileExtensionMessage = true;
                string extensions = GetExtensionsString(supportedFileExtensions);
                this.Messages.Add(new MessageItem { Author = this.Bot, Text = $"Only {extensions} files are supported.", });
            }
        }

        return addedFileNumberMessage || addedFileSizeMessage || addedFileExtensionMessage;
    }

    private void RemoveAttachedFile(object commandParameter)
    {
        int index = (int)commandParameter;
        this.AttachedFiles.RemoveAt(index);
    }

    private bool CanExecuteSendMessageCommand()
    {
        if (this.isWaitingForAIResponse)
        {
            // We are in the process of sending a message, so do not allow another send.
            return false;
        }

        string promptString = this.prompt as string;

        if (string.IsNullOrWhiteSpace(promptString))
        {
            // No prompt to send.
            return false;
        }

        if (this.AttachedFiles.Any(file => file.Guid == Guid.Empty))
        {
            // There are files that are still being uploaded.
            return false;
        }

        return true;
    }

    private async void ExecuteSendMessageCommand()
    {
        this.IsWaitingForAIResponse = true;

        string promptString = this.prompt as string;
        this.Prompt = string.Empty;
        ObservableCollection<AttachedFileData> attachedFiles = this.AttachedFiles;

        if (attachedFiles.Count != 0)
        {
            // Create a new collection without deleting the uploaded files.
            this.AttachedFiles = new ObservableCollection<AttachedFileData>();
            ObservableCollection<AttachmentData> attachments = new ObservableCollection<AttachmentData>(attachedFiles.Select(CreateAttachmentData));
            AttachmentsItem attachmentsMessage = new AttachmentsItem { Author = this.Me, Text = promptString, Attachments = attachments, };
            this.Messages.Add(attachmentsMessage);
        }
        else
        {
            MessageItem message = new MessageItem { Author = this.Me, Text = promptString, };
            this.Messages.Add(message);
        }

        if (string.IsNullOrEmpty(this.chatId))
        {
            try
            {
                this.chatId = await TelerikAIService.GenerateChatId();
            }
            catch { }

            if (string.IsNullOrEmpty(this.chatId))
            {
                MessageItem message = new MessageItem { Author = this.Bot, Text = "Sorry, I am unable to connect right now. Please try again later.", };
                this.Messages.Add(message);
                this.IsWaitingForAIResponse = false;
                return;
            }
        }

        try
        {
            string aiSuggestion = await TelerikAIService.GetAISuggestionAsync(this.chatId, promptString, attachedFiles, CancellationToken.None);
            MessageItem aiResponse = new MessageItem { Author = this.Bot, Text = aiSuggestion, };
            this.Messages.Add(aiResponse);
        }
        catch
        {
            MessageItem message = new MessageItem { Author = this.Bot, Text = "Something went wrong! Please, try again with a different prompt.", };
            this.Messages.Add(message);
        }

        this.IsWaitingForAIResponse = false;
    }
}