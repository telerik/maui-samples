using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.IO;
using System.Threading.Tasks;

namespace QSF.Examples.ImageEditorControl
{
    internal interface IImageCommandContext
    {
        ImageSource Source { get; }
        void Load(ImageSource imageSource);
        Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality);
        Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality, Size maximumSize);
        Task SaveAsync(Stream outputStream, ImageFormat imageFormat, double imageQuality, double scaleFactor);
        void Reset();
    }
}
