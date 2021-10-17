using Newtonsoft.Json.Linq;
using UnityEngine;
using GameEditor.Data;
using System.IO;
using Tools;
using static Tools.Names;
using System;

namespace GameEditor
{
    // 역할
    // - 리소스를 Json 으로부터 불러와 Storage 에 적재한다.
    // - 샌드박스명, 샌드박스 경로를 갖고 함수 호출시 반환한다.
    
    public class SandboxSaveLoader
    {
        private SandboxData _sandboxData;
        private GameObject _rootOfToy;
        private GameObject _rootOfBlock;

        private SandboxSaveLoader(SandboxData sandboxData, GameObject rootOfToy, GameObject rootOfBlock)
        {
            _sandboxData = sandboxData;
            _rootOfBlock = rootOfBlock;
            _rootOfToy = rootOfToy;
        }

        public static void SaveSandbox(SandboxData sandboxData, GameObject rootOfToy, GameObject rootOfBlock)
        {
            var sandboxSaveLoader = new SandboxSaveLoader(sandboxData, rootOfToy, rootOfBlock);
            sandboxSaveLoader.SaveSandbox();
        }
        
        private void SaveSandbox()
        {
            FileTool.CreateDirectoryIfDosentExist(SandboxChecker.GetSandboxPath(_sandboxData));
            try
            {
                SaveSandboxData();
                UpdateToysData();
                SaveToy();
            }
            catch(Exception e)
            {
                Debug.Log("Failed to create savefile at " + SandboxChecker.GetSandboxPath(_sandboxData));
                Debug.Log(e.ToString());
            }
        }

        private void SaveSandboxData() 
        {
            var jsonSandboxDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfSandboxData);
            FileTool.DeleteFileIfExist(jsonSandboxDataPath);
            var jsonSandboxData = JsonUtility.ToJson(_sandboxData);
            var stream = File.CreateText(jsonSandboxDataPath);
            stream.Write(jsonSandboxData);
            stream.Close();
        }

        private void UpdateToysData()
        {
            ToySaver.UpdateToysComponentsData(_rootOfToy);
        }
        
        private void SaveToy() 
        {
            var jsonToyDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfToyData);
            FileTool.DeleteFileIfExist(jsonToyDataPath);
            var jsonToyData = _rootOfToy.GetComponent<ToySaver>().GetJsonToysData().ToString();
            var stream = File.CreateText(jsonToyDataPath);
            stream.Write(jsonToyData);
            stream.Close();
        }

        public static GameObject LoadToy(SandboxData sandboxData)
        {
            var sandboxSaveLoader = new SandboxSaveLoader(sandboxData, null, null);
            return sandboxSaveLoader.LoadSandboxToy();
        }

        private GameObject LoadSandboxToy()
        {
            try
            {
                return LoadToy();
            }
            catch(Exception e)
            {
                Debug.Log("Failed to load with sandboxData. Id : " + _sandboxData.id);
                Debug.Log(e.ToString());
                return null;
            }
        }
        
        private GameObject LoadToy()
        {
            var jsonToyDataPath = Path.Combine(SandboxChecker.GetSandboxPath(_sandboxData), JsonNameOfToyData);
            var jsonToyData = JObject.Parse(File.ReadAllText(jsonToyDataPath));
            var loadedRootOfToy = ToyBuilder.CreateGameObject(jsonToyData);
            return loadedRootOfToy;
        }
    }
}