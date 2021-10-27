using GameEditor.Data;
using UnityEngine;

namespace GameEditor
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

        void Awake()
        {
            SandboxChecker.Initialize(Application.persistentDataPath);
            SandboxInitialize();
        }


        private void SandboxInitialize()
        {
            SetSingletonIfUnset();
            Tools.File.CreateDirectoryIfDosentExist(SandboxChecker.GetSandboxPath(sandboxData));
        }
        
        private void SetSingletonIfUnset()
        {
            if(_Sandbox == null)
                _Sandbox = this;
        }
        
        public void SetSandboxData(SandboxData sandboxData)
        {
            this.sandboxData = sandboxData;
        }
        public void SaveSandboxOnPC()
        {
            SandboxSaveLoader.SaveSandbox(sandboxData, rootOfToy, rootOfBlock);
        }

        public void LoadSandbox()
        {
            LoadImagesData();
            ReloadToy();
        }

        public void LoadImagesData()
        {
            SandboxSaveLoader.LoadImageStorageData(sandboxData);
        }

        public void ReloadToy()
        {
            Destroy(rootOfToy);
            rootOfToy = SandboxSaveLoader.LoadToy(sandboxData);
            Tools.Misc.SetChildAndParent(rootOfToy, rootOfRoots);
            
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
