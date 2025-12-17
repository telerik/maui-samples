using System.Collections.ObjectModel;

namespace QSF.Examples.ChatControl;

public class AttachmentsItem : MessageItem
{
    private ObservableCollection<AttachmentData> attachments;

    public ObservableCollection<AttachmentData> Attachments
    {
        get => this.attachments;
        set => this.UpdateValue(ref this.attachments, value);
    }
}
