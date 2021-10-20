using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Tools;
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