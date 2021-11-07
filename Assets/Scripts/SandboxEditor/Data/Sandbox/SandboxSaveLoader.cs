using System.Collections.Generic;
using System.IO;
using GameEditor.EventEditor.Controller;
using Newtonsoft.Json.Linq;
using SandboxEditor.Builder;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
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
        
        private SandboxSaveLoader(SandboxData sandboxData, GameObject rootOfToy, GameObject rootOfBlock)
        {
            _sandboxData = sandboxData;
            _rootOfBlock = rootOfBlock;
            _rootOfToy = rootOfToy;
        }
        
        private void SaveSandbox()
        {
            File.CreateDirectoryIfDoesntExist(SandboxChecker.GetSandboxPath(_sandboxData));
            SaveSandboxData();
            SaveImageStorageData();
            SaveToyStorageData();
            UpdateAndSaveToyRootData();
            SaveLatestBlockData();
            SaveConnectionData();
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

        private void SaveAudioStorageData()
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

        private void SaveLatestBlockData()
        {
            var jsonBlockDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfBlockData);
            var blockData = BlockStorage.GetLatestBlocksData(_rootOfBlock);
            var jsonBlockData = JsonUtility.ToJson(blockData, true);
            System.IO.File.WriteAllText(jsonBlockDataPath, jsonBlockData);
        }
        
        private void SaveConnectionData()
        {
            var jsonConnectionDataPath = SandboxChecker.MakeFullPath(_sandboxData, JsonNameOfConnectionData);
            var connectionData = ConnectionController.GetBlockConnections();
            var jsonConnectionData = JsonUtility.ToJson(connectionData, true);
            System.IO.File.WriteAllText(jsonConnectionDataPath, jsonConnectionData);
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