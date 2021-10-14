using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using GameEditor.Data;
using System.IO;
using System;

namespace GameEditor
{
    // 역할
    // - 리소스를 Json 으로부터 불러와 Storage 에 적재한다.
    // - 샌드박스명, 샌드박스 경로를 갖고 함수 호출시 반환한다.
    
    public class SandboxSaveLoader : MonoBehaviour
    {
        private Dictionary<int, SandboxData> _sandboxDatasOfLocal;
        private Dictionary<int, SandboxData> _sandboxDatasOfRemote;
        private SandboxData _sandboxDataOnGround;
        public static string DirectoryNameOfLocalSandbox = "LocalSandboxs";
        public static string DirectoryNameOfRemoteSandbox = "RemoteSandboxs";
        public static string JsonNameOfSandboxData = "SandboxData.json";
        public static string JsonNameOfToyData = "ToyData.json";
        public static string JsonNameOfBlockData = "BlockData.json";
        public static string RootNameOfToy = "RootOfToy";
        public static string RootNameOfBlock = "RootOfBlock";
        public string AppPath;
        public string LocalPath;
        public string RemotePath;
        public string CurrentSandboxPath {get {return GetSandboxPath(_sandboxDataOnGround);}}
        private static SandboxSaveLoader _sandboxSaveLoader;


        public static SandboxSaveLoader GetSingleton()
        {
            return _sandboxSaveLoader;
        }

        void Awake()
        {
            InitalizeField();
            SetSingletonIfUnset();
            CreateDefaultDirectoriesIfDosentExist();
            UpdateAllSandboxDataFromPC();
        }

        private void InitalizeField()
        {
            AppPath = Application.persistentDataPath;
            LocalPath = Path.Combine(AppPath,DirectoryNameOfLocalSandbox);
            RemotePath = Path.Combine(AppPath,DirectoryNameOfRemoteSandbox);
            _sandboxDatasOfLocal = new Dictionary<int, SandboxData>();
            _sandboxDatasOfRemote = new Dictionary<int, SandboxData>();
        }

        // private GameObject CreateRootOfToy()
        // {
        //     var rootOfToy = new GameObject("RootOfToy");
        //     rootOfToy.AddComponent<DataAgent>();
        //     return rootOfToy;
        // }

        private void SetSingletonIfUnset()
        {
            if(_sandboxSaveLoader == null)
                _sandboxSaveLoader = this;
        }

        private void CreateDefaultDirectoriesIfDosentExist()
        {
            CreateDirectoryIfDosentExist(AppPath);
            CreateDirectoryIfDosentExist(LocalPath);
            CreateDirectoryIfDosentExist(RemotePath);
        }

        private static void CreateDirectoryIfDosentExist(string path)
        {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void UpdateAllSandboxDataFromPC()
        {
            UpdateSandboxsData(LocalPath, _sandboxDatasOfLocal);
            UpdateSandboxsData(RemotePath, _sandboxDatasOfRemote);
        }

        private void UpdateSandboxsData(string sandboxsPath, Dictionary<int, SandboxData> sandboxDatas)
        {
            foreach(var sandboxPath in Directory.GetDirectories(sandboxsPath))
            {
                try
                {
                    var sandboxData = LoadSandboxData(Path.Combine(sandboxPath, JsonNameOfSandboxData));
                    if(!IsUpdated(sandboxData, sandboxDatas))
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

        private bool IsUpdated(SandboxData sandboxData, Dictionary<int, SandboxData> sandboxDatas)
        {
            return !sandboxDatas.ContainsKey(sandboxData.GetHashCode());
        }

        private SandboxData LoadSandboxData(string sandboxDataPath)
        {
            var rawSandboxData = File.ReadAllText(sandboxDataPath);
            return JsonUtility.FromJson<SandboxData>(rawSandboxData);
        }

        public void SaveSandboxOnGround()
        {
            SaveSandbox(_sandboxDataOnGround);
        }

        public void SaveSandbox(SandboxData sandboxData)
        {
            CreateDirectoryIfDosentExist(GetSandboxPath(sandboxData));
            try
            {
                SaveSandboxData(sandboxData);
                SaveToy(sandboxData);
            }
            catch
            {
                Debug.Log("Failed to create savefile at " + GetSandboxPath(sandboxData));
            }
        }

        private static void SaveSandboxData(SandboxData sandboxData) 
        {
            // var jsonSandboxDataPath = MakeFullPath(sandboxData, JsonNameOfSandboxData);
            // DeleteFileIfExist(jsonSandboxDataPath);
            // var jsonSandboxData = JsonUtility.ToJson(sandboxData);
            // var stream = File.CreateText(jsonSandboxDataPath);
            // stream.Write(jsonSandboxData);
            // stream.Close();
        }
        
        private static void SaveToy(SandboxData sandboxData) 
        {
            // var jsonToyDataPath = MakeFullPath(sandboxData, JsonNameOfToyData);
            // DeleteFileIfExist(jsonToyDataPath);
            // var jsonToyData = sandboxData.rootOfToy.GetComponent<DataAgent>().GetJObjectFromAll().ToString();
            // var stream = File.CreateText(jsonToyDataPath);
            // stream.Write(jsonToyData);
            // stream.Close();
        }

        private static void DeleteFileIfExist(string path)
        {
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public string GetSandboxPath(SandboxData sandboxData)
        {
            return Path.Combine(AppPath,
                sandboxData.isLocalSandbox ? DirectoryNameOfLocalSandbox : DirectoryNameOfRemoteSandbox,
                sandboxData.id.ToString());
        }

        public string MakeFullPathOfCurrentSandbox(string reletivePath)
        {
            return MakeFullPath(_sandboxDataOnGround, reletivePath);
        }

        public string MakeFullPath(SandboxData sandboxData, string reletivePath)
        {
            var sandboxPath = GetSandboxPath(sandboxData);
            return Path.Combine(sandboxPath, reletivePath);
        }

        public bool isAlreadyExistId(SandboxData sandboxData)
        {
            return isAlreadyExistId(sandboxData.id, sandboxData.isLocalSandbox);
        }

        public bool isAlreadyExistId(int id, bool isLocalSandbox)
        {
            var sandboxsPath = isLocalSandbox ? LocalPath : RemotePath;
            return Directory.Exists(Path.Combine(sandboxsPath, id.ToString()));
        }

        // Methods for debuging

        public void PrintAllSandboxData()
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