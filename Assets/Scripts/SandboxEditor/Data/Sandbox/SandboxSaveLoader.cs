using Newtonsoft.Json.Linq;
using UnityEngine;
using GameEditor.Data;
using System.IO;
using Tools;
using static Tools.Names;
using System;
using File = Tools.File;
using System.Collections.Generic;
using GameEditor.Object;
using GameEditor.Storage;

namespace GameEditor
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
            // try
            // {
                SaveSandboxData();
                SaveImageStorageData();
                UpdateAndSaveToyRootData();
            // }
            // catch(Exception e)
            // {
            //     Debug.Log("Failed to create savefile at " + SandboxChecker.GetSandboxPath(_sandboxData));
            //     throw e;
            // }
        }

        private void SaveSandboxData() 
        {
            var jsonSandboxDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfSandboxData);
            var jsonSandboxData = JsonUtility.ToJson(_sandboxData, true);
            System.IO.File.WriteAllText(jsonSandboxDataPath, jsonSandboxData);
        }

        private void SaveImageStorageData()
        {
            var jsonimageStorageDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfImageStorageData);
            var jsonimageStorageData =  JsonUtility.ToJson(ImageStorage.GetImageStorageData(),true); 
            System.IO.File.WriteAllText(jsonimageStorageDataPath, jsonimageStorageData);
        }

        private void SaveToyStorageData()
        {
            var jsonToyStorageData = JsonUtility.ToJson(ToyStorage.GetToysData(), true); 
            var jsonToyStorageDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfToyStorageData);
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
            var toyStorageData = JsonUtility.FromJson<List<ToyData>>(jsonToyStorageData.ToString());
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