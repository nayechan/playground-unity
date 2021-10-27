using UnityEngine;

namespace Tools
{
    public class Misc
    {
        public static void SetChildAndParent(GameObject child, GameObject parent)
        {
            child.transform.parent = parent.transform;
        }
    }
}