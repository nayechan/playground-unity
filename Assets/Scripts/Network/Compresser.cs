using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Tools;

namespace Network
{
    public class Compresser
    {
        private static string zipName = "sandboxZip.zip";

        public static string CreateZip(string dictionaryPath)
        {
            var zipPath = Path.Combine(dictionaryPath, zipName);
            FileTool.DeleteFileIfExist(zipPath);
            ZipFile.CreateFromDirectory(dictionaryPath, zipPath);
            return zipPath;
        }

    }
}