using System;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SDKBrowserMaui.Examples.ImageEditorControl.ToolbarCategory.StylingExample;

public partial class Styling : ContentView
{
	public Styling()
	{
		InitializeComponent();
	}

    private async void OnSaveClicked(object sender, System.EventArgs e)
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(folderPath, "image.png");
        var maxsize = new Size(200, 300);
        using (var fileStream = File.Create(filePath))
        {
            await this.imageEditor.SaveAsync(fileStream, ImageFormat.Png, 0.9, maxsize);
        }

        await Application.Current.MainPage.DisplayAlert("", "The Image is saved with Size 200:300", "OK");
    }
}