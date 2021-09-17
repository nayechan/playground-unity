using UnityEngine;

namespace GameEditor.Data
{
    [System.Serializable]
    public class TransformData : ComponentData
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        
        public void SetComponent(GameObject obj)
        {
            var tf = obj.transform;
            SetComponent(tf);
        }

        public void SetComponent(Transform tf)
        {
            tf.position = position;
            tf.rotation = (Quaternion.Euler(rotation));
            tf.localScale = scale;
        }

        public Component AddComponent(GameObject obj)
        {
            return obj.transform;
        }
        
        public void SetData(GameObject obj)
        {
            SetData(obj.transform);
        }
        public void SetData(Transform tf)
        {
            position = tf.position;
            rotation = tf.rotation.eulerAngles;
            scale = tf.localScale;
        }
        public TransformData(GameObject obj)
        {
            var tf = obj.transform;
            SetData(tf);
        }
    }
}