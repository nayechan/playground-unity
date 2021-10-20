using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Tools
{
    public static class File
    {
        public static void DeleteFileIfExist(string path)
        {
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public static void CreateDirectoryIfDosentExist(string path)
        {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static List<string> AbsolutePathsToFileNames(List<string> absolutePaths)
        {
            var fileNames = new List<string>();
            foreach(var absolutePath in absolutePaths)
                fileNames.Add(System.IO.Path.GetFileName(absolutePath));
            return fileNames;
        }

    }
}