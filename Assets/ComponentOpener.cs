using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentOpener : MonoBehaviour
{
    [SerializeField] EventEditorComponentSettings componentSettings;
    [SerializeField] EventEditorComponentSettings.ComponentType componentType;
    [SerializeField] BlockProperty blockProperty;
    // Start is called before the first frame update
    public void OpenComponent()
    {
        Debug.Log(componentType);
        componentSettings.ActivateWindow(componentType, blockProperty);
    }
}
