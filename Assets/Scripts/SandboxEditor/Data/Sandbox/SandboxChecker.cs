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
                sandboxData.id.ToString());
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

        public static bool IsAlreadyExistId(int id, bool isLocalSandbox)
        {
            var sandboxsPath = isLocalSandbox ? LocalPath : RemotePath;
            return Directory.Exists(Path.Combine(sandboxsPath, id.ToString()));
        }

        public static int CreateNonOverlappingLocalId()
        {
            int newId = new int();
            int tryLimit = 1000;
            for(int i = 0; i < tryLimit; ++i)
            {
                newId = (new System.Random()).Next(Int32.MinValue, Int32.MaxValue);
                if(SandboxChecker.IsAlreadyExistId(newId, true))
                {
                    Debug.Log($"{newId} is already exist");
                    continue;
                }
                else
                {
                    break;
                }
            }
            return newId;
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
    }
}