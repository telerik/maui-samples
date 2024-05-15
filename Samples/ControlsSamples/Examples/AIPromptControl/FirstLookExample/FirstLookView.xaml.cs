using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.RichTextEditor;

namespace QSF.Examples.AIPromptControl.FirstLookExample;

public partial class FirstLookView : RadContentView
{
    public FirstLookView()
    {
        this.InitializeComponent();

        this.Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(200), () =>
        {
            this.aiPromptButton.Popup.IsOpen = true;
        });

#if MACCATALYST || WINDOWS
        this.richTextEditor.AutoGenerateContextMenu = false;
        this.richTextEditor.ContextMenuItems.Add(new RichTextEditorSelectAllContextMenuItem());
#endif
    }
}
