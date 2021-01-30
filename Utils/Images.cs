using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;

namespace Utils
{
    public class Images
    {
        public static void Resize(HttpPostedFileBase file, string outputPath, int width, long quality = 50L)
        {
            // Use Nuget CoreCompat.System.Drawing

            Bitmap sourceImage = new Bitmap(file.InputStream);

            double widthOriginal = sourceImage.Width;
            double heightOriginal = sourceImage.Height;
            double resolution = heightOriginal / widthOriginal;
            int height = (int)(resolution * width);

            var drawArea = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(drawArea))
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingMode = CompositingMode.SourceCopy;

                graphics.DrawImage(sourceImage, 0, 0, width, height);

                using (var output = File.Open(outputPath, FileMode.Create))
                {
                    var encoderQuality = System.Drawing.Imaging.Encoder.Quality;
                    var encoderParamaters = new EncoderParameters(1);
                    encoderParamaters.Param[0] = new EncoderParameter(encoderQuality, quality);

                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    
                    drawArea.Save(output, codec, encoderParamaters);
                    output.Close();
                }

                graphics.Dispose();
            }
            sourceImage.Dispose();
        }
    }
}
