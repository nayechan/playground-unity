using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SandboxEditor.Data.Sandbox;
using MainPage.Panel;

public class SandboxInitializer : MonoBehaviour
{
    [SerializeField] LibraryPanelController libraryPanel;
    public void ReloadSandbox()
    {
        libraryPanel.UpdateComponent();
    }
    private void Awake() {
        SandboxChecker.Initialize(Application.persistentDataPath);
    }
}
