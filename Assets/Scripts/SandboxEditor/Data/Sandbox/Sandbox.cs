using System;
using System.Collections.Generic;
using GameEditor.EventEditor.Controller;
using SandboxEditor.Builder;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
using Tools;
using UnityEngine;

namespace SandboxEditor.Data.Sandbox
{
    public class Sandbox : MonoBehaviour
    {
        public SandboxData _sandboxData;
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
        public static Camera EditorCamera => _Sandbox.camera;

        private Dictionary<int, GameObject> _blockIDBlockObjectPairs;
        private Dictionary<int, GameObject> _ToyIDToyObjectPairs;

        private void Awake()
        {
            _Sandbox = this;
            SandboxChecker.Initialize(Application.persistentDataPath);
            SandboxPhaseChanger.Pause();
        }

        private void Start()
        {
            if (EditorTestMode) return;
            ReadSandboxDataFromMainScene();
            LoadSandbox();
            if (WeStartPlayRightNow()) 
                SandboxPhaseChanger.GameStart();
        }

        private static void ReadSandboxDataFromMainScene()
        {
            var newSandboxData = new SandboxData
            {
                id = PlayerPrefs.GetString("sandboxToRun"),
                isLocalSandbox = PlayerPrefs.GetInt("isLocalSandbox") == 1? true : false,
            };
            SandboxData =  SandboxSaveLoader.LoadSandboxData(newSandboxData.SandboxDataPath);
        }

        private static bool WeStartPlayRightNow()
        {
            return PlayerPrefs.GetInt("isRunningPlayer") == 1 ? true : false;
        }
        
        public void SaveSandboxOnPC()
        {
            ToySaver.UpdateToysData(RootOfToy);
            SandboxSaveLoader.SaveSandbox(SandboxData, rootOfToy, rootOfBlock);
        }
        
        public void LoadSandbox()
        {
            SandboxSaveLoader.LoadImageStorageData(SandboxData);
            SandboxSaveLoader.LoadToyStorageData(SandboxData);
            ReloadToyAndUpdateToyIDPair();
            ReloadBlockAndUpdateBlockIDPair();
            ReloadConnection(); 
        }
        
        public void ReloadToyAndUpdateToyIDPair()
        {
            Destroy(rootOfToy);
            _Sandbox._ToyIDToyObjectPairs = new Dictionary<int, GameObject>();
            rootOfToy = SandboxSaveLoader.LoadToy(SandboxData, ref _Sandbox._ToyIDToyObjectPairs);
            Misc.SetChildAndParent(rootOfToy, rootOfRoots);
        }

        private void ReloadBlockAndUpdateBlockIDPair()
        {
            BlockStorage.RenewBlockList();
            Destroy(rootOfBlock);
            (rootOfBlock, _blockIDBlockObjectPairs) = SandboxSaveLoader.LoadBlock(SandboxData);
            Misc.SetChildAndParent(rootOfBlock, rootOfRoots);
        }

        // ObjectPair 딕셔너리가 최신화된 상태에서 호출해야 정상동작합니다.
        private void ReloadConnection()
        {
            Destroy(rootOfConnectionSpriteLine);
            rootOfConnectionSpriteLine = new GameObject("SpriteLineRoot");
            Misc.SetChildAndParent(rootOfConnectionSpriteLine, rootOfRoots);
            SandboxSaveLoader.LoadConnection(SandboxData, _ToyIDToyObjectPairs, _blockIDBlockObjectPairs);
        }
        
        public static GameObject BuildSelectedToyOnToyRoot()
        {
            return selectedToyData is null ? null : BuildToyOnToyRoot(selectedToyData);
        }
        
        private static GameObject BuildToyOnToyRoot(ToyData toyData)
        {
            if (toyData is null) return null;
            _Sandbox._ToyIDToyObjectPairs = new Dictionary<int, GameObject>();
            var newToy = ToyLoader.BuildToys(toyData, ref _Sandbox._ToyIDToyObjectPairs);
            newToy.transform.parent = RootOfToy.transform;
            return newToy;
        }


    }
}
