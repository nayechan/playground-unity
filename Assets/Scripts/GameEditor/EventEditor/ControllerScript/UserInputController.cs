using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputController : MonoBehaviour
{
    public StickScript stick_L;
    public GameObject inputUis;
    public Vector2 GetStickLInput(){
        return stick_L.GetInputVector();
    }
    public void ToggleAll(){
        inputUis.SetActive(!inputUis.activeSelf);
    }
}
