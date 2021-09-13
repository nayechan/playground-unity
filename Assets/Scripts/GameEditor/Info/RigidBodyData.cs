using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEditor.Info
{
    [System.Serializable]
    public class IFRigidBody
    {
        public bool movable = false;
        public float mass = 1f;
        public float gravityScale = 1f;
        
        public void SetComponent(GameObject obj)
        {
            Rigidbody2D rb2d = obj.GetComponent<Rigidbody2D>();
            if (!rb2d)
            {
                rb2d = obj.AddComponent<Rigidbody2D>();
            }
            rb2d.mass = mass;
            rb2d.gravityScale = gravityScale;
            rb2d.bodyType = movable ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        }
    }
    
}