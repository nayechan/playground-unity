using System.Collections.Generic;
using GameEditor.EventEditor.Controller;
using SandboxEditor.Builder;
using SandboxEditor.Controller;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
using Tools;
using UnityEngine;

namespace SandboxEditor.Data.Sandbox
{
    public class Sandbox : MonoBehaviour
    {
        public GameObject _rootOfToy;
        public GameObject _rootOfBlock;
        public GameObject _rootOfConnectionSpriteLine;
        
        public SandboxData _sandboxData;
        private static SandboxData SelectedSandboxData;
        private static bool IsRunningPlayer = false;
        public static SandboxData SandboxData
        {
            get => _Sandbox._sandboxData;
            private set => _Sandbox._sandboxData = value;
        }
        public Camera camera;
        public bool EditorTestMode;
        private Dictionary<int, GameObject> _blockIDGameObjectPairs;
        private Dictionary<int, GameObject> _ToyIDGameObjectPairs;

        private static Sandbox _Sandbox;
        public static GameObject RootOfToy => _Sandbox._rootOfToy;
        public static GameObject RootOfBlock => _Sandbox._rootOfBlock;
        public static GameObject RootOfConnectionSpriteLine => _Sandbox._rootOfConnectionSpriteLine;

        public static Camera EditorCamera => _Sandbox.camera;
        public static Dictionary<int, GameObject> BlockIDGameObjectPair => _Sandbox._blockIDGameObjectPairs;
        public static Dictionary<int, GameObject> ToyIDGameObjectPair => _Sandbox._ToyIDGameObjectPairs;
        public static ToyData selectedToyData;
        
        private void Awake()
        {
            _Sandbox = this;
            SandboxChecker.Initialize(Application.persistentDataPath);
        }

        private void Start()
        {
            if (EditorTestMode) return;
            LoadSelectedSandboxData();
            LoadSandbox();
            SandboxPhaseChanger.InitializePhaseReceiverList();
            if (IsRunningPlayer) 
                SandboxPhaseChanger.StartGame();
        }

        private static void LoadSelectedSandboxData()
        {
            SandboxData = SandboxSaveLoader.LoadSandboxData(SelectedSandboxData);
        }

        public void LoadSandbox()
        {
            SandboxSaveLoader.LoadImageStorageData(SandboxData);
            SandboxSaveLoader.LoadToyRecipeStorageData(SandboxData);
            ResetObjectAndReference();
            LoadGameObjectAndAddIDReference();
        }

        public static void ResetObjectAndReference()
        {
            ResetReference();
            ResetGameObject();
        }

        private static void ResetReference()
        {
            InitializeDictionaryReference();
            BlockStorage.RenewBlockList();
            ConnectionController.RenewConnectionList();
        }

        private static void InitializeDictionaryReference()
        {
            _Sandbox._ToyIDGameObjectPairs = new Dictionary<int, GameObject>();
            _Sandbox._blockIDGameObjectPairs = new Dictionary<int, GameObject>();
        }

        private static void ResetGameObject()
        {
            Destroy(RootOfBlock);
            Destroy(RootOfToy);
            ResetConnection();
        }

        private static void ResetConnection()
        {
            Destroy(RootOfConnectionSpriteLine);
            _Sandbox._rootOfConnectionSpriteLine = new GameObject("RootOfConnection");
        }
        
        private void LoadGameObjectAndAddIDReference()
        {
            _Sandbox._rootOfToy = SandboxSaveLoader.LoadToyAndAddIDReference(SandboxData);
            _Sandbox._rootOfBlock = SandboxSaveLoader.LoadBlockAndAddIDReference(SandboxData);
            SandboxSaveLoader.LoadConnectionFromLocalData(SandboxData, _ToyIDGameObjectPairs, _blockIDGameObjectPairs);
        }
        
        public static void LoadGameObjectFromInstanceData(ToyData toyData, BlocksData blocksData, BlockConnections blockConnections)
        {
            _Sandbox._rootOfToy = ToyLoader.BuildToys(toyData);
            _Sandbox._rootOfBlock = BlockBuilder.CreateBlockRootAndAddConnectionReference(blocksData);
            ConnectionController.CreateConnectionAndAddConnectionReference
                (blockConnections, _Sandbox._ToyIDGameObjectPairs,_Sandbox._blockIDGameObjectPairs);
        }
        
        public static GameObject BuildSelectedToyOnToyRoot()
        {
            return selectedToyData is null ? null : BuildToyOnToyRoot(selectedToyData);
        }
        
        private static GameObject BuildToyOnToyRoot(ToyData toyData)
        {
            if (toyData is null) return null;
            _Sandbox._ToyIDGameObjectPairs = new Dictionary<int, GameObject>();
            var newToy = ToyLoader.BuildToys(toyData);
            newToy.transform.parent = RootOfToy.transform;
            return newToy;
        }

        
        public void SaveSandboxOnPC()
        {
            ToySaver.UpdateToysData(RootOfToy);
            SandboxSaveLoader.SaveSandbox(SandboxData, _rootOfToy, _rootOfBlock);
        }
        
        public static void SetSandboxDataToRun(string gameID, bool isLocal, bool isRunningPlayer)
        {
            SelectedSandboxData = new SandboxData()
            {
                id = gameID,
                isLocalSandbox = isLocal
            };
            IsRunningPlayer = isRunningPlayer;
        }
    }
}
