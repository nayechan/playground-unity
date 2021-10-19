using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEditor;
using GameEditor.Data;
using Network;

namespace Examples
{
    public class SandboxSaveLoaderExample : MonoBehaviour
    {
        public Sandbox sandbox;
        // Start is called before the first frame update
        void Start()
        {
            
            sandbox.SaveSandboxOnPC();
            
            sandbox.ReloadToy();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
