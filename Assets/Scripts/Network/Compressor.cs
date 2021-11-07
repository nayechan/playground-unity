using System.IO.Compression;
using System.IO;
using Tools;
using UnityEngine;
using File = Tools.File;

namespace Network
{
    public static class Compressor
    {
        public static void CreateZip(string dictionaryPath, string destinationOfZipFile)
        {
            var directoryName = Path.GetDirectoryName(destinationOfZipFile);
            File.CreateDirectoryIfDoesntExist(directoryName);
            File.DeleteFileIfExist(destinationOfZipFile);
            ZipFile.CreateFromDirectory(dictionaryPath, destinationOfZipFile);
        }

    }
}