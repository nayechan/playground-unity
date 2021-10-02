using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEditor.Info
{
    [System.Serializable]
    public class PhysicsMaterialData
    {
        public float friction = 0.4f;
        public float bounciness = 0f;
        
        public void SetComponent(GameObject obj)
        {
            // PhysicsMaterial2D pm2d = obj.GetComponent<IFObject>();
            // if (!pm2d)
            // {
            //     pm2d = new PhysicsMaterial2D();
            // }
            // pm2d.friction = friction;
            // pm2d.bounciness = bounciness;
        }
    }
    
}
