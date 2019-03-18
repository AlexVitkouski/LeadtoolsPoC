using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Drawing;

namespace LeadtoolsPoC.Converters
{
    class CommonConverter : IConverter
    {
        public CommonConverter()
        {
            LicenseManager.SetLicense();
        }

        public Image Convert(string filePath)
        {
            Image img;
            using (var codecs = new RasterCodecs())
            {

                //var stream = new MemoryStream(File.ReadAllBytes(filePath));
                //SetCodecsOptions(codecs, stream);
                //RasterImage rasterImage = codecs.Load(stream, 0, CodecsLoadByteOrder.BgrOrGray, 1, 1);

                SetCodecsOptions(codecs);
                var rasterImage = codecs.Load(filePath);
                img = RasterImageConverter.ChangeToImage(rasterImage, ChangeToImageOptions.None);
            }
            return img;
        }

        private void SetCodecsOptions(RasterCodecs codecs)
        {
            codecs.Options.Load.AllPages = false;
            codecs.Options.Load.LoadCorrupted = false;
            codecs.Options.Txt.Load.Enabled = true;

            codecs.Options.RasterizeDocument.Load.SizeMode = CodecsRasterizeDocumentSizeMode.None;
            codecs.Options.Xls.Load.MultiPageSheet = true;
            codecs.Options.Xls.Load.DisableCellClipping = true;

            // No margins (only affects RTF and TXT files)
            codecs.Options.RasterizeDocument.Load.LeftMargin = 0;
            codecs.Options.RasterizeDocument.Load.RightMargin = 0;
            codecs.Options.RasterizeDocument.Load.TopMargin = 0;
            codecs.Options.RasterizeDocument.Load.BottomMargin = 0;
            
            codecs.Options.RasterizeDocument.Load.PageWidth = 8.5;
            codecs.Options.RasterizeDocument.Load.PageHeight = 11;
            codecs.Options.RasterizeDocument.Load.Unit = CodecsRasterizeDocumentUnit.Inch;
            codecs.Options.Txt.Load.FaceName = DefaultFontFace;
            codecs.Options.Txt.Load.FontSize = DefaultFontSize;
            codecs.Options.Txt.Load.FontColor = DefaultRasterColor;
        }

        private static void SetCodecsOptions(RasterCodecs codecs, Stream fileStream)
        {
            codecs.Options.Load.AllPages = false;
            codecs.Options.Load.LoadCorrupted = false;
            codecs.Options.Txt.Load.Enabled = true;

            using (var imageInfo = codecs.GetInformation(fileStream, false))
            {
                var documentImageInfo = imageInfo.Document;

                if (documentImageInfo.IsDocumentFile)
                {
                    codecs.Options.RasterizeDocument.Load.SizeMode = CodecsRasterizeDocumentSizeMode.None;
                    codecs.Options.Xls.Load.MultiPageSheet = true;
                    codecs.Options.Xls.Load.DisableCellClipping = true;

                    // No margins (only affects RTF and TXT files)
                    codecs.Options.RasterizeDocument.Load.LeftMargin = 0;
                    codecs.Options.RasterizeDocument.Load.RightMargin = 0;
                    codecs.Options.RasterizeDocument.Load.TopMargin = 0;
                    codecs.Options.RasterizeDocument.Load.BottomMargin = 0;
                    // Generic document settings
                    //   codecs.Options.RasterizeDocument.Load.Unit = documentImageInfo.Unit;

                    codecs.Options.RasterizeDocument.Load.PageWidth = 8.5;
                    codecs.Options.RasterizeDocument.Load.PageHeight = 11;
                    codecs.Options.RasterizeDocument.Load.Unit = CodecsRasterizeDocumentUnit.Inch;

                    if (imageInfo.Format == RasterImageFormat.RasPdf)
                    {
                        codecs.Options.RasterizeDocument.Load.XResolution = DefaultResolution;
                        codecs.Options.RasterizeDocument.Load.YResolution = DefaultResolution;
                    }
                    else
                    {
                        codecs.Options.RasterizeDocument.Load.XResolution = imageInfo.XResolution;
                        codecs.Options.RasterizeDocument.Load.YResolution = imageInfo.YResolution;
                    }

                    codecs.Options.Txt.Load.FaceName = DefaultFontFace;
                    codecs.Options.Txt.Load.FontSize = DefaultFontSize;
                    codecs.Options.Txt.Load.FontColor = DefaultRasterColor;
                }
            }

            // Set the load resolution
            codecs.Options.Wmf.Load.XResolution = DefaultResolution;
            codecs.Options.Wmf.Load.YResolution = DefaultResolution;
        }

        public const int DefaultResolution = 200;
        private const string DefaultFontFace = "Consolas";
        private const int DefaultFontSize = 11;
        private static readonly RasterColor DefaultRasterColor = new RasterColor(0, 0, 0);
    }
}
