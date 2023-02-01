using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.IO;
using System.Threading.Tasks;
using Telerik.Maui.Controls;

namespace QSF.Examples.ImageEditorControl
{
    internal class ImageCommandContext : BindableObject, IImageCommandContext
    {
        public static readonly BindableProperty ImageEditorProperty =
            BindableProperty.Create(nameof(ImageEditor), typeof(RadImageEditor), typeof(ImageCommandContext));

        public RadImageEditor ImageEditor
        {
            get
            {
                return (RadImageEditor)this.GetValue(ImageEditorProperty);
            }
            set
            {
                this.SetValue(ImageEditorProperty, value);
            }
        }

        public ImageSource Source => this.ImageEditor?.Source;

        public void Load(ImageSource imageSource)
        {
            if (this.ImageEditor != null)
            {
                this.ImageEditor.Source = imageSource;
            }
        }

        public void Reset()
        {
            if (this.ImageEditor != null && this.ImageEditor.ResetCommand.CanExecute(null))
            {
                this.ImageEditor.ResetCommand.Execute(null);
            }
        }

        public async Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality)
        {
            if (this.ImageEditor != null)
            {
                await this.ImageEditor.SaveAsync(outputStream, imageFormat, imageQuality);
            }
        }

        public async Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality, Size maximumSize)
        {
            if (this.ImageEditor != null)
            {
                await this.ImageEditor.SaveAsync(outputStream, imageFormat, imageQuality, maximumSize);
            }
        }

        public async Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality, double scaleFactor)
        {
            if (this.ImageEditor != null)
            {
                await this.ImageEditor.SaveAsync(outputStream, imageFormat, imageQuality, scaleFactor);
            }
        }
    }
}
