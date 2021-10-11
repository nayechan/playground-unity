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
        private List<SandboxData> localSandboxDatas;
        private List<SandboxData> remoteSandboxDatas;
        private SandboxData currentSandboxData;
        public static string LocalSandboxDirectoryName = "LocalSandboxs";
        public static string RemoteSandboxDirectoryName = "RemoteSandboxs";
        public static string SandboxDataFileName = "SandboxData.json";
        private string AppPath {get {return Application.persistentDataPath;}}
        private string LocalPath {get {return Path.Combine(AppPath,LocalSandboxDirectoryName);}}
        private string RemotePath {get {return Path.Combine(AppPath,RemoteSandboxDirectoryName);}}
        private static SandboxSaveLoader _sandboxSaveLoader;
        public string currentSandboxPath 
        {
            get
            {
                return Path.Combine(AppPath,
                 currentSandboxData.isLocalSandbox ? LocalSandboxDirectoryName : RemoteSandboxDirectoryName,
                 currentSandboxData.id.ToString());
            }
        }

        public static SandboxSaveLoader GetSingleton()
        {
            return _sandboxSaveLoader;
        }

        void Awake()
        {
            SetSingletonIfUnset();
            CreateDirectoriesIfDosentExist();
            LoadAllSandboxData();
        }

        private void SetSingletonIfUnset()
        {
            if(_sandboxSaveLoader == null)
                _sandboxSaveLoader = this;
        }

        private void CreateDirectoriesIfDosentExist()
        {
            CreateDirectoryIfDosentExist(AppPath);
            CreateDirectoryIfDosentExist(LocalPath);
            CreateDirectoryIfDosentExist(RemotePath);
        }

        private void CreateDirectoryIfDosentExist(string path)
        {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private void LoadAllSandboxData()
        {
            LoadSandboxsData(LocalPath);
            LoadSandboxsData(RemotePath);
        }

        private void LoadSandboxsData(string sandboxsPath)
        {
            foreach(var sandboxPath in Directory.GetDirectories(sandboxsPath))
            {
                LoadSandboxData(Path.Combine(sandboxPath, SandboxDataFileName));
            }
        }

        private SandboxData LoadSandboxData(string sandboxDataPath)
        {
            try
            {
                var rawSandboxData = File.ReadAllText(sandboxDataPath);
                return JsonUtility.FromJson<SandboxData>(rawSandboxData);
            }
            catch(Exception e)
            {
                Debug.Log(e.ToString());
                throw e;
            }
        }


        public string MakeFullPath(string reletivePath)
        {
            return Path.Combine(currentSandboxPath, reletivePath);
        }

    }
}