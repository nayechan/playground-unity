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
        public GameObject rootOfLine;
        private static Sandbox _Sandbox;
        public static GameObject RootOfToy => _Sandbox.rootOfToy;
        public static GameObject RootOfBlock => _Sandbox.rootOfBlock;
        public static GameObject RootOfLine => _Sandbox.rootOfLine;

        public static ToyData selectedToyData;
        
        private Dictionary<int, GameObject> _blockIDBlockObjectPairs;
        private Dictionary<int, GameObject> _ToyIDToyObjectPairs;

        private void Awake()
        {
            SandboxChecker.Initialize(Application.persistentDataPath);
            SandboxInitialize();
        }


        private void SandboxInitialize()
        {
            _Sandbox ??= this;
            Tools.File.CreateDirectoryIfDosentExist(SandboxChecker.GetSandboxPath(sandboxData));
            // InitializeSandboxData();
            // LoadSandbox();
            PauseSandbox();
        }
        
        // MainScene(샌드박스 선택씬) 에서 선택된 샌드박스의 정보는 PlayerInfo로 전달받는다.
        public void InitializeSandboxData(SandboxData sandboxData)
        {
            this.sandboxData = sandboxData;
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
            // ReLoadConnection(); 
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

        public void ReLoadConnection()
        {
            
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
