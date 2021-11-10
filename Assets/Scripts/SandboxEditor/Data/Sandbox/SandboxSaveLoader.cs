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
            File.DeleteFileIfExist(newSandboxData.ToyStorageDataPath);
            File.DeleteFileIfExist(newSandboxData.BlockDataPath);
            File.DeleteFileIfExist(newSandboxData.SandboxDataPath);
        }
        
        private void CreateDefaultJson()
        {
            SaveJsonDataLocally(_sandboxData, _sandboxData.SandboxDataPath);
            SaveJsonDataLocally(File.DefaultJsonObject, _sandboxData.ImageDataPath);
            SaveJsonDataLocally(File.DefaultJsonObject, _sandboxData.ToyStorageDataPath);
            SaveJsonDataLocally(File.DefaultJsonObject, _sandboxData.ToyDataPath);
            SaveJsonDataLocally(File.DefaultJsonObject, _sandboxData.BlockDataPath);
            SaveJsonDataLocally(File.DefaultJsonObject, _sandboxData.ConnectionDataPath);
        }
        
        private void SaveSandbox()
        {
            File.CreateDirectoryIfDoesntExist(SandboxChecker.GetSandboxPath(_sandboxData));
            SaveJsonDataLocally(_sandboxData, _sandboxData.SandboxDataPath);
            SaveJsonDataLocally(ImageStorage.GetImageStorageData() ?? File.DefaultJsonObject, _sandboxData.ImageDataPath);
            SaveJsonDataLocally(ToyStorage.ToysData, _sandboxData.ToyStorageDataPath);
            SaveJsonDataLocally(_rootOfToy?.GetComponent<ToySaver>().GetToyData(), _sandboxData.ToyDataPath);
            SaveJsonDataLocally(BlockStorage.GetLatestBlocksData(_rootOfBlock), _sandboxData.BlockDataPath);
            SaveJsonDataLocally(ConnectionController.GetBlockConnections(), _sandboxData.ConnectionDataPath);
        }

        private void SaveJsonDataLocally(object data, string filePath)
        {
            var jsonData = JsonUtility.ToJson(data, true);
            System.IO.File.WriteAllText(filePath, jsonData);
        }

        public static SandboxData LoadSandboxData(string sandboxDataPath)
        {
            var jsonSandboxData= JObject.Parse(System.IO.File.ReadAllText(sandboxDataPath));
            var sandboxData = JsonUtility.FromJson<SandboxData>(jsonSandboxData.ToString());
            return sandboxData;
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

        public static GameObject LoadToy(SandboxData sandboxData, ref Dictionary<int, GameObject> _toyIDToyObjectPair)
        {
            var sandboxSaveLoader = new SandboxSaveLoader(sandboxData, null, null);
            return sandboxSaveLoader.LoadToy(ref _toyIDToyObjectPair);
        }
        
        private GameObject LoadToy(ref Dictionary<int, GameObject>_toyIDToyObjectPair)
        {
            var jsonToyDataPath = Path.Combine(SandboxChecker.GetSandboxPath(_sandboxData), JsonNameOfToyData);
            var jsonToyData = System.IO.File.ReadAllText(jsonToyDataPath);
            return  ToyLoader.BuildToys(jsonToyData, ref _toyIDToyObjectPair);
        }

        public static (GameObject, Dictionary<int, GameObject>) LoadBlock(SandboxData sandboxData)
        {
            var sandboxSaveLoader = new SandboxSaveLoader(sandboxData, null, null);
            return sandboxSaveLoader.LoadBlock();
        }
        
        private (GameObject, Dictionary<int, GameObject>) LoadBlock()
        {
            var jsonBlockDataPath = Path.Combine(SandboxChecker.GetSandboxPath(_sandboxData), JsonNameOfBlockData);
            var jsonBlockData = System.IO.File.ReadAllText(jsonBlockDataPath);
            var blocksData = JsonUtility.FromJson<BlocksData>(jsonBlockData);
            return BlockBuilder.CreateBlockRootAndUpdateBlockStorage(blocksData);
        }

        public static void LoadConnection(SandboxData _sandboxData, Dictionary<int, GameObject> toyIDPair, Dictionary<int, GameObject> blockIDPair )
        {
            var jsonConnectionDataPath = Path.Combine(SandboxChecker.GetSandboxPath(_sandboxData), JsonNameOfConnectionData);
            var jsonConnectionData = System.IO.File.ReadAllText(jsonConnectionDataPath);
            var connectionsData = JsonUtility.FromJson<BlockConnections>(jsonConnectionData);
            ConnectionController.CreateConnectionRootAndRenewConnections(connectionsData, toyIDPair, blockIDPair);
        }
    }
}