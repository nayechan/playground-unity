using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEditor;

namespace Examples
{
    public class SandboxSaveLoaderExample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var sandboxSaveLoader = gameObject.AddComponent<SandboxSaveLoader>();
            // Debug.Log(sandboxSaveLoader.AppPath);
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
