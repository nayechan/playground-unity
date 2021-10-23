using System.Collections;
using System.Collections.Generic;
using GameEditor.EventEditor.InputController;
using UnityEngine;

public class TouchModeSwitcher : MonoBehaviour
{
    [SerializeField] TouchController touchController;
    [SerializeField] TouchController.TouchMode touchMode;

    public void SwitchMode()
    {
        touchController.mode = touchMode;
        Debug.Log(touchController.mode);
    }
}
