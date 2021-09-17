using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    [System.Serializable]
    public class Rigidbody2DData : ComponentData
    {
        public bool movable;
        public float mass;
        public float gravityScale;
        public float linearDrag;
        public float angularDrag;
        public PhysicsMaterial2DData pm2dd;
        
        public void SetComponent(GameObject obj)
        {
            var rb2d = obj.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(rb2d);
            SetComponent(rb2d);
        }

        public void SetComponent(Rigidbody2D rb2d)
        {
            rb2d.bodyType = movable ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic; 
            rb2d.mass = mass;
            rb2d.gravityScale = gravityScale;
            rb2d.drag = linearDrag;
            rb2d.angularDrag = angularDrag;
            pm2dd.SetComponent(rb2d.sharedMaterial); 
        }
        
        public Component AddComponent(GameObject obj)
        {
            var rb2d = obj.AddComponent<Rigidbody2D>();
            rb2d.sharedMaterial = new PhysicsMaterial2D();
            return rb2d;
        }

        public Rigidbody2DData(GameObject obj)
        {
            SetData(obj);
        }
        
        public void SetData(GameObject obj)
        {
            var rb2d = obj.GetComponent<Rigidbody2D>();
            SetData(rb2d);
        }

        public void SetData(Rigidbody2D rb2d)
        {
            movable = rb2d.bodyType == RigidbodyType2D.Dynamic ? true : false;
            mass = rb2d.mass;
            gravityScale = rb2d.gravityScale;
            linearDrag = rb2d.drag;
            angularDrag = rb2d.angularDrag;
            pm2dd = new PhysicsMaterial2DData(rb2d.sharedMaterial);
        }
    }
    
}