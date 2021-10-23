using System.IO.Compression;
using System.IO;
using File = Tools.File;

namespace Network
{
    public class Compresser
    {
        private static string zipName = "sandboxZip.zip";

        public static string CreateZip(string dictionaryPath)
        {
            var zipPath = Path.Combine(dictionaryPath, zipName);
            File.DeleteFileIfExist(zipPath);
            ZipFile.CreateFromDirectory(dictionaryPath, zipPath);
            return zipPath;
        }

    }
}