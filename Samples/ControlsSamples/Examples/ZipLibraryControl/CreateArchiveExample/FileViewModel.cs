using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;

namespace QSF.Examples.ZipLibraryControl.CreateArchiveExample
{
    public class FileViewModel : ViewModelBase
    {
        private string fileExtension;
        private string fileName;
        private long fileSize;
        private bool isSelected;

        public FileViewModel(string resourceName)
        {
            this.FileName = System.IO.Path.GetFileNameWithoutExtension(resourceName);

            IResourceService resourceService = DependencyService.Get<IResourceService>();
            this.FileSize = resourceService.GetResourceSize(resourceName);

            this.FileExtension = System.IO.Path.GetExtension(resourceName);
        }

        public string FileExtension
        {
            get
            {
                return this.fileExtension;
            }
            private set
            {
                if (this.fileExtension != value)
                {
                    this.fileExtension = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            private set
            {
                if (this.fileName != value)
                {
                    this.fileName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public long FileSize
        {
            get
            {
                return this.fileSize;
            }
            private set
            {
                if (this.fileSize != value)
                {
                    this.fileSize = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
