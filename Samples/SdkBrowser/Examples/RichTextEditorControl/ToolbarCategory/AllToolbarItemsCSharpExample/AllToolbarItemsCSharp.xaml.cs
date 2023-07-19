using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using SDKBrowserMaui.Behaviors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.RichTextEditor;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.ToolbarCategory.AllToolbarItemsCSharpExample;

public partial class AllToolbarItemsCSharp : ContentView
{
	public AllToolbarItemsCSharp()
	{
		InitializeComponent();

        foreach (var item in this.CreateDefaultToolbarItems())
        {
            this.richTextToolbar.Items.Add(item);
        }

        Func<CancellationToken, Task<Stream>> streamFunc = ct => Task.Run(() =>
        {
            Assembly assembly = typeof(AllToolbarItemsCSharp).Assembly;
            string fileName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains("PickYourHoliday.html"));
            Stream stream = assembly.GetManifestResourceStream(fileName);
            return stream;
        });

        this.richTextEditor.Source = RichTextSource.FromStream(streamFunc);
        this.richTextEditor.Behaviors.Add(new PickImageBehavior());
    }

    // >> generate-richtexteditor-toolbaritems
    private IEnumerable<Telerik.Maui.Controls.ToolbarItem> CreateDefaultToolbarItems()
    {
        var defaultToolbarItems = new List<Telerik.Maui.Controls.ToolbarItem>();

        defaultToolbarItems.Add(new RichTextEditorFontFamilyToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorFontSizeToolbarItem());
        defaultToolbarItems.Add(new SeparatorToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorItalicToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorUnderlineToolbarItem());
        defaultToolbarItems.Add(new SeparatorToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorAlignLeftToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorAlignCenterToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorAlignRightToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorAlignJustifyToolbarItem());
        defaultToolbarItems.Add(new SeparatorToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorTextColorToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorHighlightTextColorToolbarItem());
        defaultToolbarItems.Add(new SeparatorToolbarItem());

        if (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop)
        {
            defaultToolbarItems.Add(new RichTextEditorAddOrEditHyperlinkToolbarItem());
            defaultToolbarItems.Add(new RichTextEditorRemoveHyperlinkToolbarItem());
            defaultToolbarItems.Add(new RichTextEditorAddOrEditImageToolbarItem());
            defaultToolbarItems.Add(new SeparatorToolbarItem());
        }

        defaultToolbarItems.Add(new RichTextEditorBulletingToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorNumberingToolbarItem());
        defaultToolbarItems.Add(new SeparatorToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorOutdentToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorIndentToolbarItem());
        defaultToolbarItems.Add(new SeparatorToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorTextFormattingToolbarItem());
        defaultToolbarItems.Add(new SeparatorToolbarItem());

        defaultToolbarItems.Add(new RichTextEditorClearFormattingToolbarItem());
        defaultToolbarItems.Add(new SeparatorToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorStrikethroughToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorSubscriptToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorSuperscriptToolbarItem());
        defaultToolbarItems.Add(new SeparatorToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorUndoToolbarItem());
        defaultToolbarItems.Add(new RichTextEditorRedoToolbarItem());

        if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
        {
            defaultToolbarItems.Add(new RichTextEditorHyperlinkNavigationToolbarItem());
            defaultToolbarItems.Add(new RichTextEditorImageNavigationToolbarItem());
        }

        if (DeviceInfo.Current.Idiom == DeviceIdiom.Tablet)
        {
            defaultToolbarItems.Add(new RichTextEditorHyperlinkNavigationToolbarItem());
            defaultToolbarItems.Add(new RichTextEditorImageNavigationToolbarItem());
        }

        return defaultToolbarItems;
    }
    // << generate-richtexteditor-toolbaritems
}