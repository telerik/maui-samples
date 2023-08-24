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
using AndroidSpecific = Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace SDKBrowserMaui.Examples.RichTextEditorControl.ToolbarCategory.AllToolbarItemsCSharpExample;

public partial class AllToolbarItemsCSharp : ContentView
{
    private AndroidSpecific.WindowSoftInputModeAdjust lastInputMode = AndroidSpecific.WindowSoftInputModeAdjust.Unspecified;

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

    protected override void OnParentSet()
    {
        base.OnParentSet();
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            if (this.Parent != null)
            {
                if (this.lastInputMode == AndroidSpecific.WindowSoftInputModeAdjust.Unspecified)
                {
                    this.lastInputMode = GetSoftInputMode();
                }

                SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust.Resize);
            }
            else
            {
                SetSoftInputMode(this.lastInputMode);
            }
        }
    }

    private static AndroidSpecific.WindowSoftInputModeAdjust GetSoftInputMode()
    {
        return AndroidSpecific.Application.GetWindowSoftInputModeAdjust(Application.Current);
    }

    private static void SetSoftInputMode(AndroidSpecific.WindowSoftInputModeAdjust inputMode)
    {
        AndroidSpecific.Application.SetWindowSoftInputModeAdjust(Application.Current, inputMode);
    }

    // >> generate-richtexteditor-toolbaritems
    private IEnumerable<Telerik.Maui.Controls.ToolbarItem> CreateDefaultToolbarItems()
    {
        var defaultToolbarItems = new List<Telerik.Maui.Controls.ToolbarItem>()
        {
            new RichTextEditorFontFamilyToolbarItem(),
            new RichTextEditorFontSizeToolbarItem(),
            new SeparatorToolbarItem(),
            new RichTextEditorBoldToolbarItem(),
            new RichTextEditorItalicToolbarItem(),
            new RichTextEditorUnderlineToolbarItem(),
            new SeparatorToolbarItem(),
            new RichTextEditorAlignLeftToolbarItem(),
            new RichTextEditorAlignCenterToolbarItem(),
            new RichTextEditorAlignRightToolbarItem(),
            new RichTextEditorAlignJustifyToolbarItem(),
            new SeparatorToolbarItem(),
            new RichTextEditorTextColorToolbarItem(),
            new RichTextEditorHighlightTextColorToolbarItem(),
            new SeparatorToolbarItem()
        };

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

        if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone || DeviceInfo.Current.Idiom == DeviceIdiom.Tablet)
        {
            defaultToolbarItems.Add(new RichTextEditorHyperlinkNavigationToolbarItem());
            defaultToolbarItems.Add(new RichTextEditorImageNavigationToolbarItem());
        }

        return defaultToolbarItems;
    }
    // << generate-richtexteditor-toolbaritems
}