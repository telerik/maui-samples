using System;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.RichTextEditor;

namespace QSF.Examples.AIPromptControl.FirstLookExample;

public partial class FirstLookView : RadContentView
{
    public FirstLookView()
    {
        this.InitializeComponent();
        this.Loaded += this.OnFirstLookViewLoaded;
#if MACCATALYST || WINDOWS
        this.richTextEditor.AutoGenerateContextMenu = false;
        this.richTextEditor.ContextMenuItems.Add(new RichTextEditorSelectAllContextMenuItem());
#endif
    }

    private void OnFirstLookViewLoaded(object sender, EventArgs e)
    {
        this.aiPromptButton.Popup.IsOpen = true;
    }
}
