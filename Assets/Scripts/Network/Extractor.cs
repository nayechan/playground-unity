using System.IO.Compression;
using System.IO;
using File = Tools.File;

namespace Network
{
    public class Extractor
    {
        public static void ExtractZip(string path)
        {
            ZipFile.ExtractToDirectory(path, Path.GetDirectoryName(path));
        }

    }
}