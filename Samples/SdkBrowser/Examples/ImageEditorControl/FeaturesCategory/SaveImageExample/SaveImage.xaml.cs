using System;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace SDKBrowserMaui.Examples.ImageEditorControl.FeaturesCategory.SaveImageExample;

public partial class SaveImage : ContentView
{
	public SaveImage()
	{
		InitializeComponent();
	}

    // >> imageeditor-saveimage-original
    private async void OnSaveOriginalClicked(object sender, EventArgs e)
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(folderPath, "image.png");
        using (var fileStream = File.Create(filePath))
        {
            await this.imageEditor.SaveAsync(fileStream, ImageFormat.Png, 0.9);
        }

        await Application.Current.MainPage.DisplayAlert("", "The Image is saved with original size", "OK");
    }
    // << imageeditor-saveimage-original

    // >> imageeditor-saveimage-specific-size
    private async void OnSaveMaxSizeClicked(object sender, EventArgs e)
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(folderPath, "image.png");
        var maxsize = new Size(400, 500);
        using (var fileStream = File.Create(filePath))
        {
            await this.imageEditor.SaveAsync(fileStream, ImageFormat.Png, 0.9, maxsize);
        }

        await Application.Current.MainPage.DisplayAlert("", "The Image is saved with Size 400:500", "OK");
    }

    // << imageeditor-saveimage-specific-size

    // >> imageeditor-saveimage-downscaled
    private async void OnSaveDownscaledClicked(object sender, EventArgs e)
    {
        var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var filePath = Path.Combine(folderPath, "image.png");
        using (var fileStream = File.Create(filePath))
        {
            await this.imageEditor.SaveAsync(fileStream, ImageFormat.Png, 0.9, 0.5);
        }

        await Application.Current.MainPage.DisplayAlert("", "The Image is downscaled to 50%", "OK");
    }
    // << imageeditor-saveimage-downscaled
}