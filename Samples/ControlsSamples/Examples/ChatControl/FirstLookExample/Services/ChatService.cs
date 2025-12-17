using Microsoft.Maui.Storage;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace QSF.Examples.ChatControl.FirstLookExample;

public class ChatService
{
    private static readonly List<Task> uploadTasks;
    private static readonly List<AttachmentData> files;
    private static List<AttachmentData> cvs;

    private bool isOperational;
    private bool hasReportedChatHistory;

    static ChatService()
    {
        uploadTasks = new List<Task>();
        files = new List<AttachmentData>();

        foreach (string fileName in ConversationConstants.mainAttachmentFileNames)
        {
            var attachment = new AttachmentData { Name = fileName };
            files.Add(attachment);
            uploadTasks.Add(UploadFileAsync(attachment));
        }

        cvs = new List<AttachmentData>();

        foreach (string fileName in ConversationConstants.cvAttachmentFileNames)
        {
            var attachment = new AttachmentData { Name = fileName };
            cvs.Add(attachment);
            uploadTasks.Add(UploadFileAsync(attachment));
        }
    }

    public bool IsOperational
    {
        get => this.isOperational;
        set
        {
            if (this.isOperational != value)
            {
                this.isOperational = value;
                this.OnIsOperationalChanged();
            }
        }
    }

    public event EventHandler<ChatResponseEventArgs> ResponseReceived;
    public event EventHandler<ChatRecipientTypingEventArgs> IsRecipientTypingChanged;

    public async void Send(string text, IEnumerable attachedFiles)
    {
        this.SendRecipientTyping(true);

        await Task.Delay(ConversationConstants.typingIndicatorDefaultDelayMs);

        if (attachedFiles != null)
        {
            this.SendResponse(ConversationConstants.chooseFilesOrLinkPromptAfterAttachments);
        }
        else
        {
            string lowerText = text?.ToLowerInvariant();
            if (string.IsNullOrEmpty(lowerText))
            {
                this.SendRecipientTyping(false);
                return;
            }

            if (lowerText.Contains(ConversationConstants.keywordAttachment) || lowerText.Contains(ConversationConstants.keywordFile))
            {
                await Task.WhenAll(uploadTasks);
                this.SendResponse(ConversationConstants.attachmentResponsePrimaryText, files);
                await Task.Delay(ConversationConstants.typingIndicatorAttachmentPromptDelayMs);
                this.SendResponse(ConversationConstants.attachmentResponseSecondaryText, cvs);
            }
            else if (lowerText.Contains(ConversationConstants.keywordLink))
            {
                string response = string.Format(ConversationConstants.linkResponseText, ConversationConstants.linkResponseUrl);
                this.SendResponse(response);
            }
            else
            {
                this.SendResponse(ConversationConstants.defaultResponseText);
            }
        }

        this.SendRecipientTyping(false);
    }

    private static async Task UploadFileAsync(AttachmentData attachment)
    {
        string assetPath = $"{ConversationConstants.demoFilesFolder}/{attachment.Name}";
        using Stream stream = await FileSystem.OpenAppPackageFileAsync(assetPath);

        if (stream.CanSeek)
        {
            attachment.Size = stream.Length;
            attachment.Guid = await DataFileService.UploadFile(stream);
        }
        else
        {
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;
            attachment.Size = ms.Length;
            attachment.Guid = await DataFileService.UploadFile(ms);
        }
    }

    private async void OnIsOperationalChanged()
    {
        if (this.hasReportedChatHistory)
        {
            return;
        }

        this.hasReportedChatHistory = true;
        this.SendRecipientTyping(true);

        await Task.Delay(ConversationConstants.typingIndicatorDefaultDelayMs);
        this.SendResponse(ConversationConstants.initialMessageText);
        await Task.Delay(ConversationConstants.typingIndicatorDefaultDelayMs);
        this.SendResponse(ConversationConstants.secondaryMessageText);

        this.SendRecipientTyping(false);
    }

    private void SendResponse(string message, List<AttachmentData> attachments = null)
    {
        if (!this.IsOperational)
        {
            return;
        }

        this.ResponseReceived?.Invoke(this, new ChatResponseEventArgs(message, attachments));
    }

    private void SendRecipientTyping(bool value)
    {
        if (!this.IsOperational)
        {
            return;
        }

        this.IsRecipientTypingChanged?.Invoke(this, new ChatRecipientTypingEventArgs(value));
    }
}