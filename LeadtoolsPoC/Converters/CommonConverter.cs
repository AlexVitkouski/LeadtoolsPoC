using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            RasterCodecs codecs = new RasterCodecs();
            var rasterImage = codecs.Load(filePath);
            var img = RasterImageConverter.ChangeToImage(rasterImage, ChangeToImageOptions.None);
            return img;
        }
    }
}
