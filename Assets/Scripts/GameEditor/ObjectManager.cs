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
        GameObject CreateObject(ObjectInfo info){
            GameObject obj = new GameObject();
            obj.AddComponent<Rigidbody2D>();
            obj.AddComponent<Collider2D>();
            obj.AddComponent<SpriteRenderer>();
            
            return obj;
        }

        void Start(){
            ObjectInfo oi = new ObjectInfo();
            oi._trans = transform;
            string st = JsonUtility.ToJson(oi, true);
            Debug.Log(st);
            ObjectInfo oi2 = JsonUtility.FromJson<ObjectInfo>(st);
            Debug.Log(JsonUtility.ToJson(oi2));
            Debug.Log(oi == oi2);
        }
    }
}
