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

        public static void PlaceToyAtViewPoint(GameObject toy)
        {
            if (Camera.main is null) return;
            toy.transform.position = Vector3.Scale(Camera.main.transform.position, new Vector3(1f, 1f, 0f));
            Debug.Log(toy.transform.position);
        }
        
        public static void PlaceToyAtTouchPoint(GameObject toy, Touch touch)
        {
            if (Camera.main is null) return;
            toy.transform.position = Vector3.Scale(Camera.main.ScreenToWorldPoint(touch.position),
                                                    new Vector3(1f, 1f, 0f));
            Debug.Log(toy.transform.position);
        }
    }
}