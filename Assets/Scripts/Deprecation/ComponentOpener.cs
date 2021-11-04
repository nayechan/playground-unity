using UnityEngine;

namespace Deprecation
{
    public class ComponentOpener : MonoBehaviour
    {
        [SerializeField] EventEditorComponentSettings componentSettings;
        [SerializeField] EventEditorComponentSettings.ComponentType componentType;
        // Start is called before the first frame update
        public void OpenComponent()
        {
            Debug.Log(componentType);
            componentSettings.ActivateWindow(componentType);
        }
    }
}
