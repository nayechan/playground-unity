using Newtonsoft.Json.Linq;
using UnityEngine;
using GameEditor.Data;
using System.IO;
using Tools;
using static Tools.Names;

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
                SaveToy();
            }
            catch
            {
                Debug.Log("Failed to create savefile at " + SandboxChecker.GetSandboxPath(_sandboxData));
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
        
        private void SaveToy() 
        {
            var jsonToyDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfToyData);
            FileTool.DeleteFileIfExist(jsonToyDataPath);
            var jsonToyData = _rootOfToy.GetComponent<DataAgent>().GetJObjectFromAll().ToString();
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
            catch
            {
                Debug.Log("Failed to load with sandboxData : " + _sandboxData.ToString());
                return null;
            }
        }
        
        private GameObject LoadToy()
        {
            var jsonToyDataPath = Path.Combine(SandboxChecker.GetSandboxPath(_sandboxData), JsonNameOfToyData);
            var jsonToyData = JObject.Parse(File.ReadAllText(jsonToyDataPath));
            var loadedRootOfToy = DataManager.CreateGameObject(jsonToyData);
            return loadedRootOfToy;
        }
    }
}