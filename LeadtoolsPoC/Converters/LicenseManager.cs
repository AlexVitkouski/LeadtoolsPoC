using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leadtools;
using Leadtools.Codecs;

namespace LeadtoolsPoC.Converters
{
    internal static class LicenseManager
    {
        public static void SetLicense()
        {
            try
            {
                const string developerKey = "hcxJXspTjJacbQDDFdE4k4CRWcAQLjIIuN383qJptTO6oJXYamPF1y6YxnqoCmFEbpyVcruaYiTZpLRGoOGYl0gjG6F4n07u";
                var pathToLicense = @"..\..\..\License\eval-license-files.lic";
                RasterSupport.SetLicense(
                    Path.GetFullPath(pathToLicense),
                    developerKey
                );

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
            }
        }
    }
}
