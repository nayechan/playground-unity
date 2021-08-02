using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputController : MonoBehaviour
{
    public StickScript stick_L;
    public GameObject inputUIs;
    public Vector2 GetStickLInput(){
        return stick_L.GetInputVector();
    }

    public void ToggleAll(){
        inputUIs.SetActive(!inputUIs.activeSelf);
    }
}
