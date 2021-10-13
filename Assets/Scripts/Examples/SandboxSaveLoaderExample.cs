using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEditor;
using GameEditor.Data;

namespace Examples
{
    public class SandboxSaveLoaderExample : MonoBehaviour
    {
        // Start is called before the first frame update
        public SandboxData sandboxData;
        public GameObject rootOfToy;
        public GameObject rootOfBlock;

        void Start()
        {
            var sandboxSaveLoader = gameObject.AddComponent<SandboxSaveLoader>();
            sandboxData.SetRootGameObjects(rootOfToy, rootOfBlock);
            sandboxData.description = "Taehyeong's Test";
            SandboxSaveLoader.SaveSandbox(sandboxData);
            sandboxSaveLoader.UpdateAllSandboxDataFromPC();
            sandboxSaveLoader.PrintAllSandboxData();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
