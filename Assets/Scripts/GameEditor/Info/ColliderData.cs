using System;
using UnityEditor;
using UnityEngine;

namespace GameEditor.Info
{
    [System.Serializable]
    public class IFCollider
    {
        public ColliderType colType = ColliderType.Box;
        public bool collidable = true, isTrigger = false;
        public float colRadius = 0.5f;
        public Vector2 colSize = Vector2.one;
        
        // this 에 기록된 정보를 토대로 obj에 컴포넌트를 추가합니다.
        // 이미 해당 컴포넌트가 있는 경우 값을 바꿉니다.
        public void SetComponent(GameObject obj)
        {
            Collider2D cd2d = obj.GetComponent<Collider2D>();
            GameObject.Destroy(cd2d);
            switch(colType)
            {
                case ColliderType.Box:
                BoxCollider2D box2d = obj.AddComponent<BoxCollider2D>();
                box2d.size = colSize;
                break;
                case ColliderType.Circle:
                CircleCollider2D cir2d = obj.AddComponent<CircleCollider2D>();
                cir2d.radius = colRadius;
                break;
            }

            cd2d = obj.GetComponent<Collider2D>();
            cd2d.enabled = collidable;
        }

        // 오브젝트의 Collider2D를 ifCol 인자에 복사해 넣는 함수입니다.
        public static bool GetInfo(GameObject obj, IFCollider ifCol)
        {
            Collider2D col2d = obj.GetComponent<Collider2D>();
            if (col2d is BoxCollider2D)
            {
                ifCol.colType = ColliderType.Box;
                BoxCollider2D box2d = (BoxCollider2D)col2d;
                ifCol.colSize = box2d.size;
            }
            else if (col2d is CircleCollider2D)
            {
                ifCol.colType = ColliderType.Circle;
                CircleCollider2D cir2d = (CircleCollider2D) col2d;
                ifCol.colRadius = cir2d.radius;
            }
            else
            {
                Debug.Log("Not Supported Collider Type");
                return false;
            }
            ifCol.collidable = col2d.enabled;
            ifCol.isTrigger = col2d.isTrigger;
            return true;
        }
    }
    
}