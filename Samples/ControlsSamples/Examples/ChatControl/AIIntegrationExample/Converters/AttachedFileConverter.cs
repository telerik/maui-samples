using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace QSF.Examples.ChatControl.AIIntegrationExample;

public class AttachedFileConverter : IChatAttachedFileConverter
{
    private static AttachedFileConverter instance;

    public static AttachedFileConverter Instance => instance ??= new AttachedFileConverter();

    public ChatAttachedFile ConvertToChatAttachedFile(object dataItem, ChatAttachedFileConverterContext context)
    {
        AttachedFileData data = (AttachedFileData)dataItem;
        ChatAttachedFile chatAttachedFile = new ChatAttachedFile { Data = data, FileName = data.Name, FileSize = data.Size };
        return chatAttachedFile;
    }

    public object ConvertToDataItem(IFileInfo fileToAttach, ChatAttachedFileConverterContext context)
    {
        return CreateAttachedFileData(fileToAttach);
    }

    internal static AttachedFileData CreateAttachedFileData(IFileInfo file)
    {
        return new AttachedFileData { Name = file.FileName, Size = file.FileSize, GetStream = file.OpenReadAsync, };
    }
}