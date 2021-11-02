using GameEditor.EventEditor.Controller;
using SandboxEditor.Builder;
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
        private static Sandbox _Sandbox;
        public static GameObject RootOfToy => _Sandbox.rootOfToy;
        public static GameObject RootOfBlock => _Sandbox.rootOfBlock;

        public static ToyData selectedToyData;

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
            ReloadToy();
        }

        private void LoadImageStorageData()
        {
            SandboxSaveLoader.LoadImageStorageData(sandboxData);
        }

        private void LoadToyStorageData()
        {
            SandboxSaveLoader.LoadToyStorageData(sandboxData);
        }
        public void ReloadToy()
        {
            Destroy(rootOfToy);
            rootOfToy = SandboxSaveLoader.LoadToy(sandboxData);
            Tools.Misc.SetChildAndParent(rootOfToy, rootOfRoots);
        }

        private static GameObject BuildToyOnToyRoot(ToyData toyData)
        {
            if (toyData is null) return null;
            var newToy = ToyLoader.BuildToys(toyData);
            newToy.transform.parent = RootOfToy.transform;
            return newToy;
        }

        public static GameObject BuildSelectedToyOnToyRoot()
        {
            return selectedToyData is null ? null : BuildToyOnToyRoot(selectedToyData);
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
