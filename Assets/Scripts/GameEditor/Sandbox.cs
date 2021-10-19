using System.Diagnostics;
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

    void Awake()
    {
        SandboxChecker.Initialize(Application.persistentDataPath);
    }

    public void SetSandboxData(SandboxData sandboxData)
    {
        this.sandboxData = sandboxData;
    }
    public void SaveSandboxOnPC()
    {
        SandboxSaveLoader.SaveSandbox(sandboxData, rootOfToy, rootOfBlock);
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

}
