using UnityEngine;

namespace GameEditor.Info
{
    [System.Serializable]
    public class IFTransform
    {
        public Vector3 position = Vector3.zero, rotation = Vector3.zero, scale = Vector3.one;
        
        public void SetComponent(GameObject obj)
        {
            obj.transform.position = position;
            obj.transform.rotation = (Quaternion.Euler(rotation));
            obj.transform.localScale = scale;
        }
    }
}