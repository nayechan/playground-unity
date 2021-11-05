using System;
using System.Collections.Generic;
using System.IO;
using Tools;
using UnityEngine;
using static Tools.Names;
using File = Tools.File;

namespace SandboxEditor.Data.Sandbox
{
    
    public static class SandboxChecker
    {
        private static Dictionary<int, SandboxData> _sandboxDatasOfLocal = new Dictionary<int, SandboxData>();
        private static Dictionary<int, SandboxData> _sandboxDatasOfRemote = new Dictionary<int, SandboxData>();
        public static string AppPath;
        public static string LocalPath;
        public static string RemotePath;
        

        public static void Initialize(string AppPath)
        {
            SetPath(AppPath);
            CreateDefaultDirectoriesIfDosentExist();
            UpdateAllSandboxDataFromPC();
        }

        private static void SetPath(string appPath)
        {
            AppPath = appPath;
            LocalPath = Path.Combine(AppPath, DirectoryNameOfLocalSandbox);
            RemotePath = Path.Combine(AppPath, DirectoryNameOfRemoteSandbox);
        }

        private static void CreateDefaultDirectoriesIfDosentExist()
        {
            File.CreateDirectoryIfDosentExist(AppPath);
            File.CreateDirectoryIfDosentExist(LocalPath);
            File.CreateDirectoryIfDosentExist(RemotePath);
        }


        public static void UpdateAllSandboxDataFromPC()
        {
            UpdateSandboxsData(LocalPath, _sandboxDatasOfLocal);
            UpdateSandboxsData(RemotePath, _sandboxDatasOfRemote);
        }

        private static void UpdateSandboxsData(string sandboxsPath, Dictionary<int, SandboxData> sandboxDatas)
        {
            foreach(var sandboxPath in Directory.GetDirectories(sandboxsPath))
            {
                try
                {
                    var sandboxData = LoadSandboxData(Path.Combine(sandboxPath, Names.JsonNameOfSandboxData));
                    if(IsNewest(sandboxData, sandboxDatas))
                        continue;
                    sandboxDatas.Add(sandboxData.GetHashCode(), sandboxData);
                    Debug.Log("SandboxData Added, Path : " + GetSandboxPath(sandboxData));
                }
                catch
                {
                    Debug.Log($"Failed to Load {Path.Combine(sandboxPath, JsonNameOfSandboxData)}. ");
                    continue;
                }
            }
        }

        private static bool IsNewest(SandboxData sandboxData, Dictionary<int, SandboxData> sandboxDatas)
        {
            return sandboxDatas.ContainsKey(sandboxData.GetHashCode());
        }

        private static SandboxData LoadSandboxData(string sandboxDataPath)
        {
            var rawSandboxData = System.IO.File.ReadAllText(sandboxDataPath);
            return JsonUtility.FromJson<SandboxData>(rawSandboxData);
        }

        public static string GetSandboxPath(SandboxData sandboxData)
        {
            return Path.Combine(AppPath,
                sandboxData.isLocalSandbox ? DirectoryNameOfLocalSandbox : DirectoryNameOfRemoteSandbox,
                sandboxData.id);
        }

        public static string MakeFullPath(Sandbox sandbox, string reletivePath)
        {
            return MakeFullPath(sandbox.sandboxData, reletivePath);
        }

        public static string MakeFullPath(SandboxData sandboxData, string reletivePath)
        {
            var sandboxPath = GetSandboxPath(sandboxData);
            return Path.Combine(sandboxPath, reletivePath);
        }

        public static bool IsAlreadyExistId(SandboxData sandboxData)
        {
            return IsAlreadyExistId(sandboxData.id, sandboxData.isLocalSandbox);
        }

        public static bool IsAlreadyExistId(string id, bool isLocalSandbox)
        {
            var sandboxsPath = isLocalSandbox ? LocalPath : RemotePath;
            return Directory.Exists(Path.Combine(sandboxsPath, id));
        }

        public static string CreateNonOverlappingLocalId()
        {
            DateTime dateTime = DateTime.UtcNow;
            string newID = "-1";

            int newIDPrefix = new int();
            int tryLimit = 1000;
            for(int i = 0; i < tryLimit; ++i)
            {
                newIDPrefix = (new System.Random()).Next(10000000, 99999999);

                newID = dateTime.ToString("yyyyMMddhhmmss")+"_"+newIDPrefix;
                Debug.Log(newID);

                if(SandboxChecker.IsAlreadyExistId(newID, true))
                {
                    Debug.Log($"{newID} is already exist");
                    continue;
                }
                else
                {
                    break;
                }
            }
            return newID;
        }

        // Methods for debuging

        public static void PrintAllSandboxData()
        {
            foreach(var sandboxData in _sandboxDatasOfLocal)
            {
                sandboxData.ToString();
            }
            foreach(var sandboxData in _sandboxDatasOfRemote)
            {
                sandboxData.ToString();
            }
        }

        public static Dictionary<int, SandboxData> getLocalSandboxList()
        {
            return _sandboxDatasOfLocal;
        }
        public static Dictionary<int, SandboxData> getRemoteSandboxList()
        {
            return _sandboxDatasOfRemote;
        }
    }
}