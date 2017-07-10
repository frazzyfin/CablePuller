using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CablePuller.Utils
{
    class PDFUtils
    {
        const int PDFQUALITY = 100; // Quality of converted image, measured in DPI

        // Given a path to a PDF file, converts it to a PNG, and returns the path to the picture
        public static string convertPDFtoPNG(string path)
        {
            MagickReadSettings settings = new MagickReadSettings();
            // Settings the density to 300 dpi will create an image with a better quality
            settings.Density = new Density(PDFQUALITY, PDFQUALITY);

            using (MagickImageCollection images = new MagickImageCollection())
            {
                // Add all the pages of the pdf file to the collection
                images.Read(path, settings);

                // Empty the write directory before we create the PNGs
                System.IO.DirectoryInfo di = new DirectoryInfo("ConvertedImages");
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                int page = 1;
                foreach (MagickImage image in images)
                {
                    // Write page to file that contains the page number
                    image.Write(@"ConvertedImages\pdfimg" + page + ".png");
                    page++;
                }

                return System.IO.Path.GetFullPath(@"ConvertedImages\pdfimg1 .png");
            }
        }
    }
}
