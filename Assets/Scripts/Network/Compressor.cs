using System.IO.Compression;
using System.IO;
using Tools;
using File = Tools.File;

namespace Network
{
    public static class Compressor
    {
        public static void CreateZip(string dictionaryPath, string destinationOfZipFile)
        {
            File.CreateDirectoryIfDoesntExist(destinationOfZipFile);
            File.DeleteFileIfExist(destinationOfZipFile);
            ZipFile.CreateFromDirectory(dictionaryPath, destinationOfZipFile);
        }

    }
}