using SkiaSharp;
using Telerik.Windows.Documents.Extensibility;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Export;

namespace QSF.Examples.WordsProcessingControl.Converters
{
    public class SkiaImageConverter : JpegImageConverterBase
    {
        public override bool TryConvertToJpegImageData(byte[] imageData, ImageQuality imageQuality, out byte[] jpegImageData)
        {
            using (SKBitmap decodedBitmap = SKBitmap.Decode(imageData))
            {
                using (SKBitmap bitmap = new SKBitmap(decodedBitmap.Width, decodedBitmap.Height))
                {
                    using (SKCanvas bitmapCanvas = new SKCanvas(bitmap))
                    {
                        SKPaint paint = new SKPaint
                        {
                            IsAntialias = true,
                            Shader = SKShader.CreateColor(new SKColor(255, 255, 255))
                        };

                        using (paint)
                        {
                            bitmapCanvas.DrawRect(new SKRect(0, 0, decodedBitmap.Width, decodedBitmap.Height), paint);
                        }

                        bitmapCanvas.DrawBitmap(decodedBitmap, new SKPoint(0, 0));
                    }

                    using (SKData encodedData = bitmap.Encode(SKEncodedImageFormat.Jpeg, (int)imageQuality))
                    {
                        jpegImageData = encodedData.ToArray();
                    }
                }
            }

            return true;
        }
    }
}
