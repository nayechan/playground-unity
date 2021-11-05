using UnityEngine;

namespace SandboxEditor.InputControl.InEditor
{
    public class TouchModeSwitcher : MonoBehaviour
    {
        [SerializeField] TouchMode touchMode;

        public void SwitchMode()
        {
            TouchController.Mode = touchMode;
            Debug.Log(TouchController.Mode);
        }
    }
}
