using System.IO;
using Newtonsoft.Json.Linq;
using SandboxEditor.Builder;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
using UnityEngine;
using static Tools.Names;
using File = Tools.File;

namespace SandboxEditor.Data.Sandbox
{
    
    public class SandboxSaveLoader
    {
        private SandboxData _sandboxData;
        private GameObject _rootOfToy;
        private GameObject _rootOfBlock;

        public static void SaveSandbox(SandboxData sandboxData, GameObject rootOfToy, GameObject rootOfBlock)
        {
            var sandboxSaveLoader = new SandboxSaveLoader(sandboxData, rootOfToy, rootOfBlock);
            sandboxSaveLoader.SaveSandbox();
        }
        
        private SandboxSaveLoader(SandboxData sandboxData, GameObject rootOfToy, GameObject rootOfBlock)
        {
            _sandboxData = sandboxData;
            _rootOfBlock = rootOfBlock;
            _rootOfToy = rootOfToy;
        }
        
        private void SaveSandbox()
        {
            File.CreateDirectoryIfDosentExist(SandboxChecker.GetSandboxPath(_sandboxData));
            SaveSandboxData();
            SaveImageStorageData();
            SaveToyStorageData();
            UpdateAndSaveToyRootData();
        }

        private void SaveSandboxData() 
        {
            var jsonSandboxDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfSandboxData);
            var jsonSandboxData = JsonUtility.ToJson(_sandboxData, true);
            System.IO.File.WriteAllText(jsonSandboxDataPath, jsonSandboxData);
        }

        private void SaveImageStorageData()
        {
            var jsonImageStorageDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfImageStorageData);
            var jsonImageStorageData =  JsonUtility.ToJson(ImageStorage.GetImageStorageData(),true); 
            System.IO.File.WriteAllText(jsonImageStorageDataPath, jsonImageStorageData);
        }

        private void SaveToyStorageData()
        {
            var jsonToyStorageDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfToyStorageData);
            var jsonToyStorageData =  JsonUtility.ToJson(ToyStorage.ToysData,true); 
            System.IO.File.WriteAllText(jsonToyStorageDataPath, jsonToyStorageData);
        }


        private void UpdateAndSaveToyRootData()
        {
            UpdateToyRootData();
            SaveToyRoot();
        }

        private void UpdateToyRootData()
        {
            ToySaver.UpdateToysData(_rootOfToy);
        }
        
        private void SaveToyRoot() 
        {
            var jsonToyDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfToyData);
            var jsonToyData = _rootOfToy.GetComponent<ToySaver>().GetJsonToyData();
            System.IO.File.WriteAllText(jsonToyDataPath, jsonToyData);
        }

        public static void LoadImageStorageData(SandboxData sandboxData)
        {
            var jsonImageStorageDataPath = Path.Combine(SandboxChecker.GetSandboxPath(sandboxData), JsonNameOfImageStorageData);
            var jsonImageStorageData = JObject.Parse(System.IO.File.ReadAllText(jsonImageStorageDataPath));
            var imageStorageData = JsonUtility.FromJson<ImagesData>(jsonImageStorageData.ToString());
            foreach(var imageData in imageStorageData.imagesData)
                ImageStorage.UpdateImagesDataAndSprites(imageData);
        }

        public static void LoadToyStorageData(SandboxData sandboxData)
        {
            var jsonToyStorageDataPath = Path.Combine(SandboxChecker.GetSandboxPath(sandboxData), JsonNameOfToyStorageData);
            var jsonToyStorageData = JObject.Parse(System.IO.File.ReadAllText(jsonToyStorageDataPath));
            var toyStorageData = JsonUtility.FromJson<ToysData>(jsonToyStorageData.ToString());
            foreach(var toyData in toyStorageData)
                ToyStorage.AddToyData(toyData);
        }

        public static GameObject LoadToy(SandboxData sandboxData)
        {
            var sandboxSaveLoader = new SandboxSaveLoader(sandboxData, null, null);
            return sandboxSaveLoader.LoadToy();
        }
        
        private GameObject LoadToy()
        {
            var jsonToyDataPath = Path.Combine(SandboxChecker.GetSandboxPath(_sandboxData), JsonNameOfToyData);
            var jsonToyData = System.IO.File.ReadAllText(jsonToyDataPath);
            var newToy = ToyLoader.BuildToys(jsonToyData);
            return newToy;
        }
        
    }
}