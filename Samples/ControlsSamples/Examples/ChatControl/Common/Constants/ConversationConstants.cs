namespace QSF.Examples.ChatControl;

internal static class ConversationConstants
{
    internal const int maxNumberOfFiles = 4;
    internal const int maxFileSizeInBytes = 5 * 1024 * 1024;
    internal const int typingIndicatorDefaultDelayMs = 1200;
    internal const int typingIndicatorAttachmentPromptDelayMs = 1000;
    internal const string meAuthorId = "person_1";
    internal const string recipientAuthorId = "person_2";
    internal const string linkResponseUrl = "https://demos.telerik.com/kendo-ui/content/chat/Portfolio.pdf";
    internal const string linkResponseText = "Here you go, mate {0} \nLet me know if you have any problem opening it.";
    internal const string initialMessageText = "Hey Jamie, do you have a minute? I've been working on my job application documents and could use a second opinion. Mind taking a look and telling me what you think?";
    internal const string secondaryMessageText = "I can send you the files or a link - which one works best for you?";
    internal const string chooseFilesOrLinkPromptAfterAttachments = "What is this Jamie? I will check it out later - please choose whether to send you 'files' or a 'link'.";
    internal const string attachmentResponsePrimaryText = "Cool. I'll send them now. Thanks";
    internal const string attachmentResponseSecondaryText = "Sorry, I forgot to upload my CV.";
    internal const string defaultResponseText = "Please choose whether to send you the files or a link.";
    internal const string toastMaxFilesFormat = "You can attach up to {0} files.";
    internal const string toastMaxFileSizeFormat = "The maximum file size is {0}MB.";
    internal const string toastBundledFileNotFoundFormat = "Bundled file not found: {0}";
    internal const string keywordAttachment = "attachment";
    internal const string keywordFile = "file";
    internal const string keywordLink = "link";

#if IOS || MACCATALYST
    internal const string demoFilesFolder = "Chat/FirstLookExample";
#else
    internal const string demoFilesFolder = "Resources/Chat/FirstLookExample";
#endif

    internal const string aiAuthorId = "person";
    internal const string aiBotAuthorId = "ai_assistant";
    internal const int aiMaxNumberOfFiles = 2;
    internal const int aiMaxFileSizeInBytes = 2 * 1024 * 1024;
    internal const string aiInitialMessage =
    "Hello! I'm your AI assistant. How can I help you today? " +
    "If you ask me a question, I will do my best to provide a clear and concise response. " +
    "I can also help you with analyzing up to 2 images per prompt up to 2MB each. *";
    internal const string aiQuickActionProvideCVTemplate = "Provide CV Template";
    internal const string aiQuickActionGenerateCoverLetter = "Generate a cover letter";
    internal const string aiUserPromptProvideCVTemplate = "Provide CV Template";
    internal const string aiUserPromptGenerateCoverLetter = "Generate a cover letter";
    internal const string aiProvideCVTemplateResponse = "Sure. Here is a CV template file in PDF format. Can I help you with something else? *";
    internal const string aiProvideCoverLetterResponse = "Here's a professional cover letter template for you to download and customize. Let me know if you need adjustments. *";
    internal const string aiUnableToProvideFileFormat = "Unable to provide the requested file ({0}) right now. Please try again later. *";
    internal const string aiFileLimitExceededFormat = "You can attach up to {0} files per prompt. *";
    internal const string aiMaxFileSizeFormat = "The maximum file size is {0}MB. *";
    internal const string aiSupportedExtensionsFormat = "Only {0} files are supported. *";
    internal const string aiConnectionErrorMessage = "Sorry, I am unable to connect right now. Please try again later. *";
    internal const string aiGenericErrorMessage = "Something went wrong! Please, try again with a different prompt. *";
    internal const string aiDescribePhotoPrompt = "Describe my photo";
    internal const string aiCvFileName = "CV.pdf";
    internal const string aiCoverLetterFileName = "CoverLetter.pdf";

    internal static readonly string[] mainAttachmentFileNames =
    {
        "Portfolio.pdf",
        "CoverLetter.pdf"
    };

    internal static readonly string[] cvAttachmentFileNames =
    {
        "CV.pdf"
    };

    internal static readonly string[] aiSupportedFileExtensions = { ".png", ".jpg", ".jpeg" };
    internal static readonly string[] aiPredefinedPhotos = { "RadImageEditor.png", "SampleImage6.jpg", "SampleImage2.jpg" };
}