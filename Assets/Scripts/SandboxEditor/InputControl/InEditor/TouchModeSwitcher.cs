using UnityEngine;

namespace SandboxEditor.InputControl.InEditor
{
    public class TouchModeSwitcher : MonoBehaviour
    {
        [SerializeField] TouchController.TouchMode touchMode;

        public void SwitchMode()
        {
            TouchController.Mode = touchMode;
            Debug.Log(TouchController.Mode);
        }
    }
}
