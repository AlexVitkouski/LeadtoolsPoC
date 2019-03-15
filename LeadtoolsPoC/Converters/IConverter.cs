using System.Drawing;

namespace LeadtoolsPoC.Converters
{
    interface IConverter
    {
        Image Convert(string filePath);
    }
}
