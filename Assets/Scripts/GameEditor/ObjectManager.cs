using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor
{
    public class ObjectManager : MonoBehaviour
    {
        /*
        하는 일
        1. 오브젝트 생성, 삭제, 속성 편집
        2. 오브젝트 정보 기록
        */

        void Start(){
            ObjectInfo info = new ObjectInfo();
            info.texturePath = "Common/Brown Stony";
            info.movable = true;
            info.position = transform.position;
            info.pixelsPerUnit = 512f;
            info.colSize = Vector2.one;
            CreateObject(info);
        }

        GameObject CreateObject(ObjectInfo info){
            GameObject obj = new GameObject();
            Rigidbody2D rb2d = obj.AddComponent<Rigidbody2D>();
            SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
            
            SetTransform(obj.transform, info);
            SetSprite(sr, info);
            SetCollider(obj, info);
            SetRigidBody(rb2d, info);

            return obj;
        }

        void SetTransform(Transform trans, ObjectInfo info)
        {
            trans.position = info.position;
            trans.rotation = (Quaternion.Euler(info.rotation));
            trans.localScale = info.scale;
        }

        void SetSprite(SpriteRenderer sr, ObjectInfo info)
        {
            sr.color = info.color;
            Texture2D tex = Resources.Load<Texture2D>(info.texturePath);
            if(tex == null) { Debug.Log("Can't find texture : " + info.texturePath);}
            sr.sprite = Sprite.Create(tex, new Rect(0f, 0f , tex.width, tex.height), new Vector2(0.5f, 0.5f), info.pixelsPerUnit);

            if(!info.visible) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        }

        void SetCollider(GameObject obj, ObjectInfo info)
        {
            switch(info.colType)
            {
                case ColliderType.Box:
                BoxCollider2D box2d = obj.AddComponent<BoxCollider2D>();
                box2d.size = info.colSize;
                break;
                case ColliderType.Circle:
                CircleCollider2D cir2d = obj.AddComponent<CircleCollider2D>();
                cir2d.radius = info.colRadius;
                break;
            }
            PhysicsMaterial2D pm2d = new PhysicsMaterial2D();
            pm2d.friction = info.friction;
            pm2d.bounciness = info.bounciness;
            Collider2D cd2d = obj.GetComponent<Collider2D>();
            cd2d.sharedMaterial = pm2d;

            cd2d.enabled = info.collidable;
        }

        void SetRigidBody(Rigidbody2D rb2d, ObjectInfo info)
        {
            rb2d.bodyType = info.movable? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
            rb2d.mass = info.weight;
            rb2d.gravityScale = info.gravity;
            PhysicsMaterial2D pm2d = new PhysicsMaterial2D();
            pm2d.friction = info.friction;
            pm2d.bounciness = info.bounciness;
            rb2d.sharedMaterial = pm2d;
        }
    }
}
