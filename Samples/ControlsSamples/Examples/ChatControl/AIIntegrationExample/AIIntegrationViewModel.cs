using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
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
    private static int predefinedPhotosIndex = -1;

    private string chatId;
    private object prompt;
    private bool isWaitingForAIResponse;
    private Command sendMessageCommand;
    private ObservableCollection<AttachedFileData> attachedFiles;

    public AIIntegrationViewModel()
    {
        this.Me = ConversationConstants.aiAuthorId;
        this.Bot = ConversationConstants.aiBotAuthorId;
        this.AttachFilesCommand = new Command(this.AttachFiles);
        this.RemoveAttachedFileCommand = new Command(this.RemoveAttachedFile);
        this.sendMessageCommand = new Command(async () => await this.ExecuteSendMessageCommand(), () => this.CanExecuteSendMessageCommand());

        this.AttachedFiles = new ObservableCollection<AttachedFileData>();

        var messages = new ObservableCollection<object>
        {
            new MessageItem { Author = this.Bot, Text = ConversationConstants.aiInitialMessage }
        };
        this.Messages = messages;

        this.QuickActions = new ObservableCollection<string>
        {
            ConversationConstants.aiQuickActionProvideCVTemplate,
            ConversationConstants.aiQuickActionGenerateCoverLetter
        };
        this.QuickActionCommand = new Command<string>(this.ExecuteQuickAction);

        this.SetPredefinedPrompt();
    }

    public object Me { get; }
    public object Bot { get; }
    public ObservableCollection<string> QuickActions { get; }
    public ICommand QuickActionCommand { get; }
    public ObservableCollection<object> Messages { get; }
    public ICommand SendMessageCommand => this.sendMessageCommand;
    public ICommand AttachFilesCommand { get; }
    public ICommand RemoveAttachedFileCommand { get; }

    public ObservableCollection<AttachedFileData> AttachedFiles
    {
        get => this.attachedFiles;
        private set => this.UpdateValue(ref this.attachedFiles, value, this.OnAttachedFilesChanged);
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

    private static AttachmentData CreateAttachmentData(AttachedFileData attachedFile) =>
        new() { Name = attachedFile.Name, Size = attachedFile.Size, Guid = attachedFile.Guid };

    private static string GetExtensionsString(IEnumerable<string> extensions)
    {
        var builder = new StringBuilder();
        int i = 0;
        var list = extensions.ToList();
        foreach (var ext in list)
        {
            if (i > 0)
            {
                builder.Append(i < list.Count - 1 ? ", " : " and ");
            }
            builder.Append(ext);
            i++;
        }
        return builder.ToString();
    }

    private void ExecuteQuickAction(string label)
    {
        if (string.IsNullOrEmpty(label))
        {
            return;
        }

        if (label == ConversationConstants.aiQuickActionProvideCVTemplate)
        {
            _ = this.SendPredefinedFileAsync(
                userPrompt: ConversationConstants.aiUserPromptProvideCVTemplate,
                assetFileName: ConversationConstants.aiCvFileName,
                aiText: ConversationConstants.aiProvideCVTemplateResponse);
            return;
        }

        if (label == ConversationConstants.aiQuickActionGenerateCoverLetter)
        {
            _ = this.SendPredefinedFileAsync(
                userPrompt: ConversationConstants.aiUserPromptGenerateCoverLetter,
                assetFileName: ConversationConstants.aiCoverLetterFileName,
                aiText: ConversationConstants.aiProvideCoverLetterResponse);
            return;
        }

        this.Prompt = label;
        if (this.sendMessageCommand.CanExecute(null))
        {
            this.sendMessageCommand.Execute(null);
        }
    }

    private async Task SendPredefinedFileAsync(string userPrompt, string assetFileName, string aiText)
    {
        this.Messages.Add(new MessageItem { Author = this.Me, Text = userPrompt });

        try
        {
            var attachment = await this.CreateServerFileAttachmentFromAssetAsync(assetFileName);
            var attachmentsCollection = new ObservableCollection<AttachmentData> { attachment };

            var aiMessage = new AttachmentsItem
            {
                Author = this.Bot,
                Text = aiText,
                Attachments = attachmentsCollection
            };
            this.Messages.Add(aiMessage);
        }
        catch (IOException)
        {
            this.Messages.Add(new MessageItem
            {
                Author = this.Bot,
                Text = string.Format(ConversationConstants.aiUnableToProvideFileFormat, assetFileName)
            });
        }
    }

    private async Task<AttachmentData> CreateServerFileAttachmentFromAssetAsync(string assetFileName)
    {
        string assetPath = $"{ConversationConstants.demoFilesFolder}/{assetFileName}";

        using Stream assetStream = await FileSystem.OpenAppPackageFileAsync(assetPath);
        var ms = new MemoryStream();
        await assetStream.CopyToAsync(ms);
        ms.Position = 0;
        long size = ms.Length;

        Guid guid = await DataFileService.UploadFile(ms);

        return new AttachmentData
        {
            Name = assetFileName,
            Size = size,
            Guid = guid
        };
    }

    private void SetPredefinedPrompt()
    {
        if (predefinedPhotosIndex == -1)
        {
            var rnd = new Random();
            predefinedPhotosIndex = rnd.Next(ConversationConstants.aiPredefinedPhotos.Length);
        }

        string photo = ConversationConstants.aiPredefinedPhotos[predefinedPhotosIndex % ConversationConstants.aiPredefinedPhotos.Length];
        predefinedPhotosIndex++;

        Func<Stream> getStreamFunc = () =>
        {
            Assembly assembly = typeof(AIIntegrationViewModel).Assembly;
            string file = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains(photo));
            return assembly.GetManifestResourceStream(file);
        };

        long fileSize;
        using (Stream stream = getStreamFunc()) { fileSize = stream.Length; }

        this.AttachedFiles.Add(new AttachedFileData
        {
            Name = photo,
            Size = fileSize,
            GetStream = () => Task.Run(getStreamFunc)
        });

        this.Prompt = ConversationConstants.aiDescribePhotoPrompt;
    }

    private void OnIsWaitingForAIResponseChanged(bool oldValue) => this.sendMessageCommand.ChangeCanExecute();

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
            foreach (AttachedFileData f in args.NewItems)
            {
                await this.TryUploadFile(f);
            }
        }
        else if (args.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (AttachedFileData f in args.OldItems)
            {
                this.TryDeleteFile(f);
            }
        }

        this.sendMessageCommand.ChangeCanExecute();
    }

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
            else if (guid != Guid.Empty)
            {
                DataFileService.DeleteFile(guid);
            }
        }
    }

    private void TryDeleteFile(AttachedFileData attachedFileData) =>
        DataFileService.DeleteFile(attachedFileData.Guid);

    private void AttachFiles(object commandParameter)
    {
        var filesToAttach = (IList<AttachedFileData>)commandParameter;
        bool hasInvalid = this.PrintFileLimitationsMessages(filesToAttach);

        if (!hasInvalid)
        {
            foreach (var f in filesToAttach)
            {
                this.AttachedFiles.Add(f);
            }
        }

        filesToAttach.Clear();
    }

    private bool PrintFileLimitationsMessages(IList<AttachedFileData> filesToAttach)
    {
        bool numberWarning = false;
        bool sizeWarning = false;
        bool extensionWarning = false;

        int newTotal = this.AttachedFiles.Count + filesToAttach.Count;
        if (newTotal > ConversationConstants.aiMaxNumberOfFiles)
        {
            numberWarning = true;
            this.Messages.Add(new MessageItem { Author = this.Bot, Text = string.Format(ConversationConstants.aiFileLimitExceededFormat, ConversationConstants.aiMaxNumberOfFiles) });
        }

        foreach (var f in filesToAttach)
        {
            if (f.Size > ConversationConstants.aiMaxFileSizeInBytes && !sizeWarning)
            {
                sizeWarning = true;
                int maxMB = ConversationConstants.aiMaxFileSizeInBytes / (1024 * 1024);
                this.Messages.Add(new MessageItem { Author = this.Bot, Text = string.Format(ConversationConstants.aiMaxFileSizeFormat, maxMB) });
            }

            string ext = Path.GetExtension(f.Name)?.ToLowerInvariant();
            if (!ConversationConstants.aiSupportedFileExtensions.Contains(ext) && !extensionWarning)
            {
                extensionWarning = true;
                string extensions = GetExtensionsString(ConversationConstants.aiSupportedFileExtensions);
                this.Messages.Add(new MessageItem { Author = this.Bot, Text = string.Format(ConversationConstants.aiSupportedExtensionsFormat, extensions) });
            }
        }

        return numberWarning || sizeWarning || extensionWarning;
    }

    private void RemoveAttachedFile(object commandParameter)
    {
        int index = (int)commandParameter;
        if (index >= 0 && index < this.AttachedFiles.Count)
        {
            this.AttachedFiles.RemoveAt(index);
        }
    }

    private bool CanExecuteSendMessageCommand()
    {
        if (this.isWaitingForAIResponse)
        {
            return false;
        }

        string promptString = this.prompt as string;
        if (string.IsNullOrWhiteSpace(promptString))
        {
            return false;
        }

        if (this.AttachedFiles.Any(f => f.Guid == Guid.Empty))
        {
            return false;
        }

        return true;
    }

    private async Task ExecuteSendMessageCommand()
    {
        this.IsWaitingForAIResponse = true;

        string promptString = this.prompt as string;
        this.Prompt = string.Empty;
        var currentFiles = this.AttachedFiles;

        if (currentFiles.Count != 0)
        {
            this.AttachedFiles = new ObservableCollection<AttachedFileData>();
            var attachments = new ObservableCollection<AttachmentData>(currentFiles.Select(CreateAttachmentData));
            var attachmentsMessage = new AttachmentsItem { Author = this.Me, Text = promptString, Attachments = attachments };
            this.Messages.Add(attachmentsMessage);
        }
        else
        {
            this.Messages.Add(new MessageItem { Author = this.Me, Text = promptString });
        }

        if (string.IsNullOrEmpty(this.chatId))
        {
            try { this.chatId = await TelerikAIService.GenerateChatId(); } catch { }
            if (string.IsNullOrEmpty(this.chatId))
            {
                this.Messages.Add(new MessageItem { Author = this.Bot, Text = ConversationConstants.aiConnectionErrorMessage });
                this.IsWaitingForAIResponse = false;
                return;
            }
        }

        try
        {
            string aiSuggestion = await TelerikAIService.GetAISuggestionAsync(this.chatId, promptString, currentFiles, CancellationToken.None);
            this.Messages.Add(new MessageItem { Author = this.Bot, Text = aiSuggestion });
        }
        catch
        {
            this.Messages.Add(new MessageItem { Author = this.Bot, Text = ConversationConstants.aiGenericErrorMessage });
        }

        this.IsWaitingForAIResponse = false;
    }
}