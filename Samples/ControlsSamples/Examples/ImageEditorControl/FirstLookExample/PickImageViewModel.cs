using Microsoft.Maui.Controls;
using QSF.ViewModels;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QSF.Examples.ImageEditorControl.FirstLookExample
{
    public class PickImageViewModel : ExampleViewModel
    {
        private bool isBusy;

        public PickImageViewModel()
        {
            this.Images = new ObservableCollection<ImageModel>();
            this.OpenCommand = new Command<string>(OpenImage);
            this.LoadImagesAsync();
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            private set
            {
                if (this.isBusy != value)
                {
                    this.isBusy = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ImageModel> Images { get; }

        public ICommand OpenCommand { get; }

        private async void LoadImagesAsync()
        {
            this.IsBusy = true;
            this.Images.Clear();

            var imagePaths = await StorageHelper.ExtractResourcesAsync("ImageEditor", "FirstLookExample");

            foreach (var imagePath in imagePaths)
            {
#if ANDROID || IOS
                if (imagePath.EndsWith("jpg"))
                {
                    continue;
                }
#else
                if (imagePath.EndsWith("png"))
                {
                    continue;
                }
#endif
                this.Images.Add(new ImageModel()
                {
                    ImagePath = imagePath,
                    ImageSource = new StreamImageSource()
                    {
                        Stream = (_) =>
                        {
                            return Task.FromResult<Stream>(File.OpenRead(imagePath));
                        }
                    }
                });
            }

            this.IsBusy = false;
        }

        private void OpenImage(string imagePath)
        {
            NavigationHelper.Instance.NavigateToEditImageView(imagePath);
        }
    }
}
