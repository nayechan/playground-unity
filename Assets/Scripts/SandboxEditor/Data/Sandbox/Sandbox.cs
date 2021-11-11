using System;
using System.Collections.Generic;
using GameEditor.EventEditor.Controller;
using SandboxEditor.Builder;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
using UnityEngine;

namespace SandboxEditor.Data.Sandbox
{
    public class Sandbox : MonoBehaviour
    {
        public SandboxData _sandboxData;
        private static SandboxData SelectedSandboxData;
        private static bool IsRunningPlayer = false;
        public static SandboxData SandboxData
        {
            get => _Sandbox._sandboxData;
            private set => _Sandbox._sandboxData = value;
        }

        public GameObject rootOfRoots;
        public GameObject rootOfToy;
        public GameObject rootOfBlock;
        public GameObject rootOfConnectionSpriteLine;
        public Camera camera;
        public bool EditorTestMode;
        private static Sandbox _Sandbox;
        public static GameObject RootOfToy => _Sandbox.rootOfToy;
        public static GameObject RootOfBlock => _Sandbox.rootOfBlock;
        public static GameObject RootOfConnectionSpriteLine => _Sandbox.rootOfConnectionSpriteLine;

        public static ToyData selectedToyData;
        public static Camera Camera
        {
            get => _Sandbox.camera;
            set => _Sandbox.camera = value;
        }

        private Dictionary<int, GameObject> _blockIDBlockObjectPairs;
        private Dictionary<int, GameObject> _ToyIDToyObjectPairs;

        private void Awake()
        {
            _Sandbox = this;
            SandboxChecker.Initialize(Application.persistentDataPath);
            SandboxInitialize();
        }

        private void SandboxInitialize()
        {
            PauseSandbox();
        }

        private void Start()
        {
            if (EditorTestMode) return;
            LoadSandboxData();
            LoadSandbox();
            if (IsRunningPlayer) 
                SandboxPhase.GameStart();
        }

        private static void LoadSandboxData()
        {
            SandboxData =  SandboxSaveLoader.LoadSandboxData(SelectedSandboxData.SandboxDataPath);
        }

        private static void PauseSandbox()
        {
            SandboxPhase.Pause();
        }
        
        public void SaveSandboxOnPC()
        {
            UpdateToyRootData();
            SandboxSaveLoader.SaveSandbox(SandboxData, rootOfToy, rootOfBlock);
        }

        
        private void UpdateToyRootData()
        {
            ToySaver.UpdateToysData(RootOfToy);
        }
        
        public void LoadSandbox()
        {
            LoadImageStorageData();
            LoadToyStorageData();
            ReloadToyAndUpdateToyIDPair();
            ReloadBlockAndUpdateBlockIDPair();
            ReloadConnection(); 
        }

        private void LoadImageStorageData()
        {
            SandboxSaveLoader.LoadImageStorageData(SandboxData);
        }

        private void LoadToyStorageData()
        {
            SandboxSaveLoader.LoadToyStorageData(SandboxData);
        }
        public void ReloadToyAndUpdateToyIDPair()
        {
            Destroy(rootOfToy);
            _Sandbox._ToyIDToyObjectPairs = new Dictionary<int, GameObject>();
            rootOfToy = SandboxSaveLoader.LoadToy(SandboxData, ref _Sandbox._ToyIDToyObjectPairs);
            Tools.Misc.SetChildAndParent(rootOfToy, rootOfRoots);
        }

        private void ReloadBlockAndUpdateBlockIDPair()
        {
            BlockStorage.RenewBlockList();
            Destroy(rootOfBlock);
            (rootOfBlock, _blockIDBlockObjectPairs) = SandboxSaveLoader.LoadBlock(SandboxData);
            Tools.Misc.SetChildAndParent(rootOfBlock, rootOfRoots);
        }

        private static GameObject BuildToyOnToyRoot(ToyData toyData)
        {
            if (toyData is null) return null;
            _Sandbox._ToyIDToyObjectPairs = new Dictionary<int, GameObject>();
            var newToy = ToyLoader.BuildToys(toyData, ref _Sandbox._ToyIDToyObjectPairs);
            newToy.transform.parent = RootOfToy.transform;
            return newToy;
        }

        public static GameObject BuildSelectedToyOnToyRoot()
        {
            return selectedToyData is null ? null : BuildToyOnToyRoot(selectedToyData);
        }

        // 두개의 딕셔너리가 최신화 되려면 토이, 블록을 먼저 불러와야 함.
        private void ReloadConnection()
        {
            Destroy(rootOfConnectionSpriteLine);
            rootOfConnectionSpriteLine = new GameObject("SpriteLineRoot");
            Tools.Misc.SetChildAndParent(rootOfConnectionSpriteLine, rootOfRoots);
            SandboxSaveLoader.LoadConnection(SandboxData, _ToyIDToyObjectPairs, _blockIDBlockObjectPairs);
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
