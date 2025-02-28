using System.Collections.Generic;
using System.IO;

namespace Tools
{
    public static class File
    {
        public const string BackgroundMusicPath = "Sound/BackgroundMusic";
        public const string EffectSoundPath = "Sound/EffectSound";
        public static readonly object DefaultJsonObject = new object();
        
        public static void DeleteFileIfExist(string path)
        {
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public static void CreateDirectoryIfDoesntExist(string path)
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