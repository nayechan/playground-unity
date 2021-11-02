using UnityEngine;

namespace Tools
{
    public class Misc
    {
        public static void SetChildAndParent(GameObject child, GameObject parent)
        {
            child.transform.parent = parent.transform;
        }

        public static void DisableChildrenRigidBody(GameObject toyRoot)
        {
            foreach (var rigidbody2D in toyRoot.GetComponentsInChildren<Rigidbody2D>())
                rigidbody2D.simulated = false;
        }
        
        public static void EnableChildrenRigidBody(GameObject toyRoot)
        {
            foreach (var rigidbody2D in toyRoot.GetComponentsInChildren<Rigidbody2D>())
                rigidbody2D.simulated = true;
        }

        public static void DisableChildrenBlock(GameObject blockRoot)
        {
            
        }
        
        public static void EnableChildrenBlock(GameObject blockRoot)
        {
            
        }
    }
}