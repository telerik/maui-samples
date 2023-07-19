using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.IO;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.SignaturePad;

namespace SDKBrowserMaui.Examples.SignaturePadControl.SavingOptionsCategory.SavingOptionsExample;

public partial class SaveSignatureAsImage : ContentView
{
	public SaveSignatureAsImage()
	{
		InitializeComponent();
    }

    private async void GetJPEG_Clicked(object sender, EventArgs e)
    {
        // >> signaturepad-saving-jpeg
        var settings = new SaveImageSettings()
        {
            ImageFormat = Telerik.Maui.Controls.SignaturePad.ImageFormat.Jpeg,
            ScaleFactor = 0.7,
            ImageQuality = 1,
            BackgroundColor = Colors.LightGray,
            StrokeColor = Colors.DarkBlue,
            StrokeThickness = 5
        };

        byte[] array;

        using (var stream = new MemoryStream())
        {
            await this.signaturePad.SaveImageAsync(stream, settings);
            array = stream.ToArray();

            this.signatureImage.Source = ImageSource.FromStream((token) => Task.FromResult((Stream)new MemoryStream(array)));
        }
        // << signaturepad-saving-jpeg
    }

    private async void GetPNG_Clicked(object sender, EventArgs e)
    { 
        // >> signaturepad-saving-png
        var settings = new SaveImageSettings()
        {
            ImageFormat = Telerik.Maui.Controls.SignaturePad.ImageFormat.Png,
            BackgroundColor = Colors.LightGray,
            StrokeColor = Colors.DarkRed,
            StrokeThickness = 5
        };

        byte[] array;

        using (var stream = new MemoryStream())
        {
            await this.signaturePad.SaveImageAsync(stream, settings);
            array = stream.ToArray();

            this.signatureImage.Source = ImageSource.FromStream((token) => Task.FromResult((Stream)new MemoryStream(array)));
        }
        // << signaturepad-saving-png
    }
}