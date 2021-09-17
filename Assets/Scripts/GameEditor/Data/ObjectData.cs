using UnityEngine;

namespace GameEditor.Data
{
    [System.Serializable]
    public class ObjectData
    {
        public string name;
        public int id;
        public TransformData tfd;

        public ObjectData(GameObject obj)
        {
            name = obj.name;
            id = obj.GetInstanceID();
            tfd = new TransformData(obj);
        }
    }
}