using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEditor;

public class SandboxInitializer : MonoBehaviour
{
    public void ReloadSandbox()
    {
        
    }
    private void Awake() {
        SandboxChecker.Initialize(Application.persistentDataPath);
    }
}
