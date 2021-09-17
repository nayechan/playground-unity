using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    [System.Serializable]
    public class CircleCollider2DData : ComponentData
    {
        public bool collidable, isTrigger;
        public float colRadius;
        public Vector2 offset;
        public PhysicsMaterial2DData pm2dd;
        
        public void SetComponent(GameObject obj)
        {
            var cir2d = obj.GetComponent<CircleCollider2D>();
            Assert.IsNotNull(cir2d);
            SetComponent(cir2d);
        }
        public void SetComponent(CircleCollider2D cir2d)
        {
            cir2d.radius = colRadius;
            cir2d.enabled = collidable;
            cir2d.offset = offset;
            pm2dd.SetComponent(cir2d.sharedMaterial); 
        }

        public Component AddComponent(GameObject obj)
        {
            var cir2d = obj.AddComponent<CircleCollider2D>();
            cir2d.sharedMaterial = new PhysicsMaterial2D();
            return cir2d;
        }

        public CircleCollider2DData(GameObject obj)
        {
            var cir2d = obj.GetComponent<CircleCollider2D>();
            SetComponent(cir2d);
        }
        
        public void SetData(GameObject obj)
        {
            var cir2d = obj.GetComponent<CircleCollider2D>();
            SetComponent(cir2d);
        }

        public void SetData(CircleCollider2D cir2d)
        {
            colRadius = cir2d.radius;
            collidable = cir2d.enabled;
            isTrigger = cir2d.isTrigger;
            offset = cir2d.offset;
            pm2dd = new PhysicsMaterial2DData(cir2d.sharedMaterial);
        }
    }
}