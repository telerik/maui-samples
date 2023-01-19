using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace SDKBrowserMaui.Examples.ImageEditorControl.ToolbarCategory.CustomCropToolbarExample;

public partial class CustomCropToolbar : ContentView
{
	public CustomCropToolbar()
	{
		InitializeComponent();
    }

    // >> imageeditor-load-image-from-file
    private async void OnAddImageClicked(object sender, System.EventArgs e)
    {
        var filePicker = FilePicker.Default;
        var fileResult = await filePicker.PickAsync();
        var filePath = fileResult?.FullPath;

        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }

        var imageSource = new FileImageSource
        {
            File = filePath
        };

        this.imageEditor.Source = imageSource;
    }
    // << imageeditor-load-image-from-file
}