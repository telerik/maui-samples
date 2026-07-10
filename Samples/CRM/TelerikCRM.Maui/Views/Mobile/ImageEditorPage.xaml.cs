using Telerik.Maui.Controls.ImageEditor;
using TelerikCRM.Maui.Common;
using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views.Mobile;

public partial class ImageEditorPage
{
    private ImageEditorViewModel vm;
    private ImageFormat originalFormat = ImageFormat.Png;

    public ImageEditorPage(ImageEditorViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = this.vm = viewModel;
    }

    public Command GoBackCommand { get; }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        this.imageEditor.Source = this.vm.ImageUri.StartsWith("http")
            ? ImageSource.FromUri(new Uri(this.vm.ImageUri))
            : ImageSource.FromFile(this.vm.ImageUri);

        this.originalFormat = this.vm.ImageUri.DetermineImageFormat();
    }

    private void ImageEditorImageLoaded(object sender, ImageLoadedEventArgs e) => this.IsBusy = false;
}