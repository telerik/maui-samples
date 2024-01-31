using Microsoft.Maui.Controls.Xaml;
using System;
using Telerik.Maui.Controls;

namespace QSF.Examples.RichTextEditorControl.CustomToolbarExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomToolbarView : RadContentView
{
    public CustomToolbarView()
    {
        this.InitializeComponent();

#if !WINDOWS
         this.colorPicker.SelectionChanged += this.ColorPickerSelectionChanged;
#endif
    }

#if !WINDOWS
    private void ColorPickerSelectionChanged(object sender, EventArgs e)
    {
        this.richTextEditor.BackgroundColor = (Microsoft.Maui.Graphics.Color)this.colorPicker.SelectedItem;
    }
#endif
}