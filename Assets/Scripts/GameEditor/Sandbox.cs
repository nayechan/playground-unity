using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using GameEditor;
using GameEditor.Data;
using UnityEngine;

public class Sandbox : MonoBehaviour
{
    public SandboxData sandboxData;
    public GameObject rootOfToy;
    public GameObject rootOfBlock;

    public void SaveSandbox()
    {
        SandboxSaveLoader.SaveSandbox(sandboxData, rootOfToy, rootOfBlock);
    }

    public void ReloadToy()
    {
        Destroy(rootOfToy);
        rootOfToy = SandboxSaveLoader.LoadToy(sandboxData);
    }
    
    public string MakeFullPath(string relativePath)
    {
        return SandboxChecker.MakeFullPath(sandboxData, relativePath);
    }

}
