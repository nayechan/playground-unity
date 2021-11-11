using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using GameEditor.EventEditor.Controller;
using Newtonsoft.Json.Linq;
using SandboxEditor.Builder;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
using UnityEditor;
using UnityEngine;
using static Tools.Names;
using File = Tools.File;

namespace SandboxEditor.Data.Sandbox
{
    
    public class SandboxSaveLoader
    {
        private readonly SandboxData _sandboxData;
        private readonly GameObject _rootOfToy;
        private readonly GameObject _rootOfBlock;



        public static void SaveSandbox(SandboxData sandboxData, GameObject rootOfToy, GameObject rootOfBlock)
        {
            var sandboxSaveLoader = new SandboxSaveLoader(sandboxData, rootOfToy, rootOfBlock);
            sandboxSaveLoader.SaveSandbox();
        }
        
        private SandboxSaveLoader(SandboxData sandboxData, GameObject rootOfToy = null, GameObject rootOfBlock = null)
        {
            _sandboxData = sandboxData;
            _rootOfBlock = rootOfBlock;
            _rootOfToy = rootOfToy;
        }
        
        public static void InitializeLocalSandbox(SandboxData newSandboxData)
        {
            var sandboxSaveLoader = new SandboxSaveLoader(newSandboxData);
            File.CreateDirectoryIfDoesntExist(SandboxChecker.GetSandboxPath(newSandboxData));
            DeleteFilesIfExist(newSandboxData);
            sandboxSaveLoader.CreateDefaultJson();
        }

        private static void DeleteFilesIfExist(SandboxData newSandboxData)
        {
            File.DeleteFileIfExist(newSandboxData.ConnectionDataPath);
            File.DeleteFileIfExist(newSandboxData.ToyDataPath);
            File.DeleteFileIfExist(newSandboxData.ToyRecipeStorageDataPath);
            File.DeleteFileIfExist(newSandboxData.BlockDataPath);
            File.DeleteFileIfExist(newSandboxData.SandboxDataPath);
        }
        
        private void CreateDefaultJson()
        {
            SaveJsonDataLocally(_sandboxData, _sandboxData.SandboxDataPath);
            SaveJsonDataLocally(File.DefaultJsonObject, _sandboxData.ImageDataPath);
            SaveJsonDataLocally(File.DefaultJsonObject, _sandboxData.ToyRecipeStorageDataPath);
            SaveJsonDataLocally(File.DefaultJsonObject, _sandboxData.ToyDataPath);
            SaveJsonDataLocally(File.DefaultJsonObject, _sandboxData.BlockDataPath);
            SaveJsonDataLocally(File.DefaultJsonObject, _sandboxData.ConnectionDataPath);
        }
        
        private void SaveSandbox()
        {
            File.CreateDirectoryIfDoesntExist(SandboxChecker.GetSandboxPath(_sandboxData));
            SaveJsonDataLocally(_sandboxData, _sandboxData.SandboxDataPath);
            SaveJsonDataLocally(ImageStorage.GetImageStorageData() ?? File.DefaultJsonObject, _sandboxData.ImageDataPath);
            SaveJsonDataLocally(ToyPrefabDataStorage.ToysData, _sandboxData.ToyRecipeStorageDataPath);
            SaveJsonDataLocally(_rootOfToy?.GetComponent<ToySaver>().GetToyData(), _sandboxData.ToyDataPath);
            SaveJsonDataLocally(BlockStorage.GetLatestBlocksData(_rootOfBlock), _sandboxData.BlockDataPath);
            SaveJsonDataLocally(ConnectionController.GetBlockConnections(), _sandboxData.ConnectionDataPath);
        }

        private void SaveJsonDataLocally(object data, string filePath)
        {
            var jsonData = JsonUtility.ToJson(data, true);
            System.IO.File.WriteAllText(filePath, jsonData);
        }

        public static SandboxData LoadSandboxData(SandboxData _sandboxData)
        {
            var jsonSandboxData= JObject.Parse(System.IO.File.ReadAllText(_sandboxData.SandboxDataPath));
            var sandboxData = JsonUtility.FromJson<SandboxData>(jsonSandboxData.ToString());
            return sandboxData;
        }


        public static void LoadImageStorageData(SandboxData sandboxData)
        {
            var jsonImageStorageData = JObject.Parse(System.IO.File.ReadAllText(sandboxData.ImageDataPath));
            var imageStorageData = JsonUtility.FromJson<ImagesData>(jsonImageStorageData.ToString());
            foreach(var imageData in imageStorageData.imagesData)
                ImageStorage.UpdateImagesDataAndSprites(imageData);
        }

        public static void LoadToyRecipeStorageData(SandboxData sandboxData)
        {
            var jsonToyStorageData = JObject.Parse(System.IO.File.ReadAllText(sandboxData.ToyRecipeStorageDataPath));
            var toyStorageData = JsonUtility.FromJson<ToysData>(jsonToyStorageData.ToString());
            foreach(var toyData in toyStorageData)
                ToyPrefabDataStorage.AddToyRecipeData(toyData);
        }

        public static GameObject LoadToyAndAddIDReference(SandboxData sandboxData)
        {
            var sandboxSaveLoader = new SandboxSaveLoader(sandboxData, null, null);
            return sandboxSaveLoader.LoadToyAndUpdateIDReference();
        }
        
        private GameObject LoadToyAndUpdateIDReference()
        {
            var jsonToyData = System.IO.File.ReadAllText(_sandboxData.ToyDataPath);
            return  ToyLoader.BuildToys(jsonToyData);
        }

        public static GameObject LoadBlockAndAddIDReference(SandboxData sandboxData)
        {
            var sandboxSaveLoader = new SandboxSaveLoader(sandboxData, null, null);
            return sandboxSaveLoader.LoadBlock();
        }
        
        private GameObject LoadBlock()
        {
            var jsonBlockDataPath = Path.Combine(SandboxChecker.GetSandboxPath(_sandboxData), JsonNameOfBlockData);
            var jsonBlockData = System.IO.File.ReadAllText(jsonBlockDataPath);
            var blocksData = JsonUtility.FromJson<BlocksData>(jsonBlockData);
            return BlockBuilder.CreateBlockRootAndAddConnectionReference(blocksData);
        }

        public static void LoadConnection(SandboxData _sandboxData, Dictionary<int, GameObject> toyIDPair, Dictionary<int, GameObject> blockIDPair )
        {
            var jsonConnectionDataPath = Path.Combine(SandboxChecker.GetSandboxPath(_sandboxData), JsonNameOfConnectionData);
            var jsonConnectionData = System.IO.File.ReadAllText(jsonConnectionDataPath);
            var connectionsData = JsonUtility.FromJson<BlockConnections>(jsonConnectionData);
            ConnectionController.CreateConnectionAndAddConnectionReference(connectionsData, toyIDPair, blockIDPair);
        }
    }
}