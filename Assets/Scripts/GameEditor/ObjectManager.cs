using System.Collections;
using System.Collections.Generic;
using GameEditor.Data;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Transform = UnityEngine.Transform;

namespace GameEditor
{
    public class ObjectManager : MonoBehaviour
    {
        /*
        하는 일
        1. 오브젝트 생성, 삭제, 속성 편집
        2. 오브젝트 정보 기록
        */
        public static ObjectManager om;
        public ObjectData selectedObjectData;

        void Start(){
            if(!om)
            {
                om = this;
            }
        }

        public GameObject CreateObject(ObjectData data)
        {
            var obj = new GameObject();
            
            return obj;
        }

        // void SetTransform(Transform trans, IFObject info)
        // {
        //     trans.position = info.position;
        //     trans.rotation = (Quaternion.Euler(info.rotation));
        //     trans.localScale = info.scale;
        // }
        //
        // void SetSprite(SpriteRenderer sr, IFObject info)
        // {
        //     sr.color = info.color;
        //     Texture2D tex = Resources.Load<Texture2D>(info.texturePath);
        //     if(tex == null) { Debug.Log("Can't find texture : " + info.texturePath);}
        //     sr.sprite = Sprite.Create(tex, new Rect(0f, 0f , tex.width, tex.height), new Vector2(0.5f, 0.5f), info.pixelsPerUnit);
        //
        //     if(!info.visible) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        // }
        //
        // void SetCollider(GameObject obj, IFObject info)
        // {
        //     switch(info.colType)
        //     {
        //         case ColliderType.Box:
        //         BoxCollider2D box2d = obj.AddComponent<BoxCollider2D>();
        //         box2d.size = info.colSize;
        //         break;
        //         case ColliderType.Circle:
        //         CircleCollider2D cir2d = obj.AddComponent<CircleCollider2D>();
        //         cir2d.radius = info.colRadius;
        //         break;
        //     }
        //     PhysicsMaterial2D pm2d = new PhysicsMaterial2D();
        //     pm2d.friction = info.friction;
        //     pm2d.bounciness = info.bounciness;
        //     Collider2D cd2d = obj.GetComponent<Collider2D>();
        //     cd2d.sharedMaterial = pm2d;
        //
        //     cd2d.enabled = info.collidable;
        // }
        //
        // void SetRigidBody(Rigidbody2D rb2d, IFObject info)
        // {
        //     rb2d.bodyType = info.movable? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        //     rb2d.mass = info.weight;
        //     rb2d.gravityScale = info.gravity;
        //     PhysicsMaterial2D pm2d = new PhysicsMaterial2D();
        //     pm2d.friction = info.friction;
        //     pm2d.bounciness = info.bounciness;
        //     rb2d.sharedMaterial = pm2d;
        // }
        
        static public ObjectManager GetOM()
        {
            return om;
        }
    }
}
