using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace GameEditor.Data
{
    [System.Serializable]
    public class BoxCollider2DData : ComponentData
    {
        public bool collidable, isTrigger;
        public Vector2 size;
        public Vector2 offset;
        public PhysicsMaterial2DData pm2dd;

        public void SetComponent(GameObject obj)
        {
            var cd2d = obj.GetComponent<BoxCollider2D>();
            SetComponent(cd2d);
        }

        public void SetComponent(BoxCollider2D box2d)
        {
            box2d.size = size;
            box2d.offset = offset;
            box2d.enabled = collidable;
            pm2dd.SetComponent(box2d.sharedMaterial); 
        }

        public Component AddComponent(GameObject obj)
        {
            var box2d = obj.AddComponent<BoxCollider2D>();
            box2d.sharedMaterial = new PhysicsMaterial2D();
            return box2d;
        }

        public BoxCollider2DData(GameObject obj)
        {
            SetData(obj);
        }

        public void SetData(GameObject obj)
        {
            var box2d = obj.GetComponent<BoxCollider2D>();
            SetData(box2d);
        }

        public void SetData(BoxCollider2D box2d)
        {
            size = box2d.size;
            collidable = box2d.enabled;
            isTrigger = box2d.isTrigger;
            offset = box2d.offset;
            pm2dd = new PhysicsMaterial2DData(box2d.sharedMaterial);
        }
    }
}