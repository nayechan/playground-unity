using UnityEngine;

namespace Deprecation
{
    public class UserInputController : MonoBehaviour
    {
        public UIJoystick stick_L;
        public GameObject inputUis;
        static UserInputController _uic;

        public static UserInputController GetUserInputController(){
            return _uic;
        }

        public void Start(){
            _uic = this;
        }
        public Vector2 GetStickLInput(){
            return stick_L.GetInputVector();
        }
        public Vector2 GetTouchInputViewPort(Camera cam){
            if(Input.touchCount == 0) return Vector2.zero;
            Vector2 viewPort = cam.ScreenToViewportPoint(Input.GetTouch(0).position);
            viewPort -= new Vector2(0.5f, 0.5f);
            viewPort.Scale(new Vector2(2f,2f));
            return viewPort;
        }
    
        public void ToggleAll(){
            inputUis.SetActive(!inputUis.activeSelf);
        }

        // public void ResetCamera(){
        //     _cam = GameObject.FindObjectOfType<GameDisplayBlock>().GetComponent<Camera>();
        // }
    }
}
