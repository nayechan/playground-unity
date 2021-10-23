using GameEditor.Data;
using UnityEngine;

namespace GameEditor
{
    public class Sandbox : MonoBehaviour
    {
        public SandboxData sandboxData;
        public GameObject rootOfToy;
        public GameObject rootOfBlock;

        void Awake()
        {
            SandboxChecker.Initialize(Application.persistentDataPath);
            SandboxInitialize();
        }


        // For Test.
        private void SandboxInitialize()
        {
            Tools.File.CreateDirectoryIfDosentExist(SandboxChecker.GetSandboxPath(sandboxData));
            // SandboxSaveLoader.LoadImageStorageData(sandboxData);
        }
        // -------

        public void SetSandboxData(SandboxData sandboxData)
        {
            this.sandboxData = sandboxData;
        }
        public void SaveSandboxOnPC()
        {
            SandboxSaveLoader.SaveSandbox(sandboxData, rootOfToy, rootOfBlock);
        }

        public void LoadImagesData()
        {
            SandboxSaveLoader.LoadImageStorageData(sandboxData);
        }

        public void ReloadToy()
        {
            Destroy(rootOfToy);
            rootOfToy = SandboxSaveLoader.LoadToy(sandboxData);
        }

        public string GetSandboxPath()
        {
            return SandboxChecker.GetSandboxPath(sandboxData);
        }

        public string MakeFullPath(string relativePath)
        {
            return SandboxChecker.MakeFullPath(sandboxData, relativePath);
        }

        static public void DeactiveGameObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

    }
}
