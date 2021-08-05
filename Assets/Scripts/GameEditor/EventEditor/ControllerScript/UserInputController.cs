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
    public Vector2 GetTouchInputViewPort(){
        if(Input.touchCount == 0) return Vector2.zero;
        Vector2 viewPort = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
        viewPort -= new Vector2(0.5f, 0.5f);
        viewPort.Scale(new Vector2(2f,2f));
        return viewPort;
    }
    public void ToggleAll(){
        inputUis.SetActive(!inputUis.activeSelf);
    }
}
