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
            pm2d.friction = friction;
            pm2d.bounciness = bounciness;
        }
        public PhysicsMaterial2DData(PhysicsMaterial2D pm2d)
        {
            Assert.IsNotNull(pm2d);
            friction = pm2d.friction;
            bounciness = pm2d.bounciness;
        }
    }
    
}
