using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Tools
{
    public class FileTool
    {
        public static void DeleteFileIfExist(string path)
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void CreateDirectoryIfDosentExist(string path)
        {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}