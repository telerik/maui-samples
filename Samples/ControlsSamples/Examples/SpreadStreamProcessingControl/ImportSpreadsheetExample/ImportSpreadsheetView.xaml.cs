using System;
using Microsoft.Maui.Controls.Xaml;
using Telerik.Maui.Controls;

namespace QSF.Examples.SpreadStreamProcessingControl.ImportSpreadsheetExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ImportSpreadsheetView : RadContentView
{
    public ImportSpreadsheetView()
    {
        InitializeComponent();
        this.consoleLabel.SizeChanged += OnLabelSizeChanged;
    }
    private async void OnLabelSizeChanged(object sender, EventArgs e)
    {
        await Scroller.ScrollToAsync(0, consoleLabel.Height, true);
    }
}