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
        public SandboxData sandboxData;
        public GameObject rootOfRoots;
        public GameObject rootOfToy;
        public GameObject rootOfBlock;
        public GameObject rootOfConnectionSpriteLine;
        public Camera camera;
        public bool dontLoadPlayerPref;
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
            SandboxChecker.Initialize(Application.persistentDataPath);
            SandboxInitialize();
        }

        private void Start()
        {
            if (dontLoadPlayerPref)
            {
                LoadSandbox();
                return;
            }

            LoadSandboxDataFromMainScene();
            LoadSandbox();
            if (WeStartPlayRightNow()) 
                SandboxPhase.GameStart();

        }

        private static void LoadSandboxDataFromMainScene()
        {
            var newSandboxData = new SandboxData
            {
                id = PlayerPrefs.GetString("sandboxToRun"),
                isLocalSandbox = PlayerPrefs.GetInt("isLocalSandbox") == 1? true : false,
            };
            _Sandbox.sandboxData = newSandboxData;
        }

        private static bool WeStartPlayRightNow()
        {
            return PlayerPrefs.GetInt("isRunningPlayer") == 1 ? true : false;
        }

        private void SandboxInitialize()
        {
            _Sandbox ??= this;
            PauseSandbox();
        }

        private static void PauseSandbox()
        {
            SandboxPhase.Pause();
        }
        
        public void SaveSandboxOnPC()
        {
            SandboxSaveLoader.SaveSandbox(sandboxData, rootOfToy, rootOfBlock);
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
            SandboxSaveLoader.LoadImageStorageData(sandboxData);
        }

        private void LoadToyStorageData()
        {
            SandboxSaveLoader.LoadToyStorageData(sandboxData);
        }
        public void ReloadToyAndUpdateToyIDPair()
        {
            Destroy(rootOfToy);
            _Sandbox._ToyIDToyObjectPairs = new Dictionary<int, GameObject>();
            rootOfToy = SandboxSaveLoader.LoadToy(sandboxData, ref _Sandbox._ToyIDToyObjectPairs);
            Tools.Misc.SetChildAndParent(rootOfToy, rootOfRoots);
        }

        private void ReloadBlockAndUpdateBlockIDPair()
        {
            BlockStorage.RenewBlockList();
            Destroy(rootOfBlock);
            (rootOfBlock, _blockIDBlockObjectPairs) = SandboxSaveLoader.LoadBlock(sandboxData);
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
            SandboxSaveLoader.LoadConnection(sandboxData, _ToyIDToyObjectPairs, _blockIDBlockObjectPairs);
        }

        public string GetSandboxPath()
        {
            return SandboxChecker.GetSandboxPath(sandboxData);
        }

        public string MakeFullPath(string relativePath)
        {
            return SandboxChecker.MakeFullPath(sandboxData, relativePath);
        }

        public static void DeactiveGameObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

    }
}
