using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    [System.Serializable]
    public class PhysicsMaterial2DData
    {
        public float friction;
        public float bounciness;

        public void SetComponent(Rigidbody2D rb2d)
        {
            var pm2d = rb2d.sharedMaterial;
            Assert.IsNotNull(rb2d);
            SetComponent(pm2d);
        }

        public void SetComponent(Collider2D col2d)
        {
            var pm2d = col2d.sharedMaterial;
            Assert.IsNotNull(col2d);
            SetComponent(pm2d);
        }

        public void SetComponent(PhysicsMaterial2D pm2d)
        {
            if (pm2d == null)
            {
                friction = 0.4f;
                bounciness = 0f;
            }
            else
            {
                friction = pm2d.friction;
                bounciness = pm2d.bounciness;
            }
        }
        public PhysicsMaterial2DData(PhysicsMaterial2D pm2d)
        {
            SetComponent(pm2d);
        }
    }
    
}
