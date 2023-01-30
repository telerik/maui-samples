using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.IO;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.SignaturePad;
using ImageFormat = Telerik.Maui.Controls.SignaturePad.ImageFormat;

namespace QSF.Examples.SignaturePadControl.FirstLookExample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class FirstLookView : RadContentView
{
    RadPopup popup;
    public FirstLookView()
    {
        InitializeComponent();
        this.popup = new RadPopup();
        this.popup.Placement = PlacementMode.Center;
        this.popup.OutsideBackgroundColor = Color.FromArgb("#BF4B4C4C");
        this.popup.IsModal = true;
        this.popup.PlacementTarget = this;
    }

    private void SignaturePad_StrokeStarted(object sender, EventArgs e)
    {
        var viewModel = this.BindingContext as FirstLookViewModel;
        viewModel.IsSigned = true;
    }

    private void SignaturePad_Cleared(object sender, EventArgs e)
    {
        var viewModel = this.BindingContext as FirstLookViewModel;
        viewModel.IsSigned = false;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        await this.SaveImage(ImageFormat.Png);
    }

    private async Task SaveImage(ImageFormat format)
    {
        byte[] array;
        ImageSource imageSource;

        using (var stream = new MemoryStream())
        {
            var settings = new SaveImageSettings()
            {
                ImageFormat = format,
                BackgroundColor = Colors.Transparent,
            };
            await this.signaturePad.SaveImageAsync(stream, settings);
            array = stream.ToArray();

            imageSource = ImageSource.FromStream(() => new MemoryStream(array));
        }

        var popupContent = (View)((ControlTemplate)this.Resources["PopupControlTemplate"]).CreateContent();
        Image image = (Image)popupContent.FindByName("signatureImage");
        image.Source = imageSource;
        this.popup.Content = popupContent;
        this.popup.IsOpen = true;
    }

    private void ClearButton_Clicked(object sender, EventArgs e)
    {
        this.popup.IsOpen = false;
    }
}