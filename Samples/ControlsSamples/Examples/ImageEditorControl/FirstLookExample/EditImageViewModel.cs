using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.IO;
using System.Windows.Input;

namespace QSF.Examples.ImageEditorControl.FirstLookExample
{
    public class EditImageViewModel : ExampleViewModel
    {
        public ICommand SaveCommand { get; set; }
        public ICommand DiscardCommand { get; set; }

        private string imagePath;
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }
            set
            {
                this.imagePath = value;
                this.OnPropertyChanged();
            }
        }

        public EditImageViewModel()
        {
            this.SaveCommand = new Command<IImageCommandContext>(SaveImageAsync);
            this.DiscardCommand = new Command<IImageCommandContext>(DiscardImageAsync);
        }

        public EditImageViewModel(string path) : this()
        {
            this.ImagePath = path;
        }

        private async void SaveImageAsync(IImageCommandContext imageCommandContext)
        {
            if (imageCommandContext is null)
            {
                return;
            }

            if (imageCommandContext.Source is not FileImageSource)
            {
                return;
            }

            var filePath = (imageCommandContext.Source as FileImageSource).File;

            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            Microsoft.Maui.Graphics.ImageFormat imageFormat;
            switch (Path.GetExtension(filePath))
            {
#if ! (ANDROID || IOS)
                case ".jpg":
                    imageFormat = Microsoft.Maui.Graphics.ImageFormat.Jpeg;
                    break;
#endif
                case ".png":
                    imageFormat = Microsoft.Maui.Graphics.ImageFormat.Png;
                    break;
                case ".bmp":
                    imageFormat = Microsoft.Maui.Graphics.ImageFormat.Bmp;
                    break;
                case ".gif":
                    imageFormat = Microsoft.Maui.Graphics.ImageFormat.Gif;
                    break;
                default:
                    imageFormat = Microsoft.Maui.Graphics.ImageFormat.Png;
                    break;
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (var fileStream = File.Create(filePath))
            {
                await imageCommandContext.SaveAsync(fileStream, imageFormat, 1.0);
            }

            NavigationHelper.Instance.NavigateToPickImageView();
        }

        private async void DiscardImageAsync(IImageCommandContext imageCommandContext)
        {
            imageCommandContext.Reset();
            NavigationHelper.Instance.NavigateToPickImageView();
        }
    }
}
