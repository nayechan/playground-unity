using UnityEngine;

namespace SandboxEditor.InputControl.InEditor
{
    public class TouchModeSwitcher : MonoBehaviour
    {
        [SerializeField] TouchMode touchMode;

        public void SwitchMode()
        {
            TouchInEditor.Mode = touchMode;
            Debug.Log(TouchInEditor.Mode);
        }
    }
}
