using UnityEngine;

namespace SandboxEditor.InputControl.InEditor
{
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
}
