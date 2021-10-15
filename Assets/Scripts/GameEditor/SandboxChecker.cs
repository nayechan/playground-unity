using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using GameEditor.Data;
using System.IO;
using System;
using Tools;
using static Tools.Names;

namespace GameEditor
{
    
    public static class SandboxChecker
    {
        private static Dictionary<int, SandboxData> _sandboxDatasOfLocal = new Dictionary<int, SandboxData>();
        private static Dictionary<int, SandboxData> _sandboxDatasOfRemote = new Dictionary<int, SandboxData>();
        public static string AppPath = Application.persistentDataPath;
        public static string LocalPath = Path.Combine(AppPath, DirectoryNameOfLocalSandbox);
        public static string RemotePath = Path.Combine(AppPath, DirectoryNameOfRemoteSandbox);

        public static void Initialize(SandboxData sandboxData)
        {
            CreateDefaultDirectoriesIfDosentExist();
            UpdateAllSandboxDataFromPC();
        }

        private static void CreateDefaultDirectoriesIfDosentExist()
        {
            FileTool.CreateDirectoryIfDosentExist(AppPath);
            FileTool.CreateDirectoryIfDosentExist(LocalPath);
            FileTool.CreateDirectoryIfDosentExist(RemotePath);
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
            var rawSandboxData = File.ReadAllText(sandboxDataPath);
            return JsonUtility.FromJson<SandboxData>(rawSandboxData);
        }

        public static string GetSandboxPath(SandboxData sandboxData)
        {
            return Path.Combine(AppPath,
                sandboxData.isLocalSandbox ? DirectoryNameOfLocalSandbox : DirectoryNameOfRemoteSandbox,
                sandboxData.id.ToString());
        }

        public static string MakeFullPath(SandboxData sandboxData, string reletivePath)
        {
            var sandboxPath = GetSandboxPath(sandboxData);
            return Path.Combine(sandboxPath, reletivePath);
        }

        public static bool isAlreadyExistId(SandboxData sandboxData)
        {
            return isAlreadyExistId(sandboxData.id, sandboxData.isLocalSandbox);
        }

        public static bool isAlreadyExistId(int id, bool isLocalSandbox)
        {
            var sandboxsPath = isLocalSandbox ? LocalPath : RemotePath;
            return Directory.Exists(Path.Combine(sandboxsPath, id.ToString()));
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