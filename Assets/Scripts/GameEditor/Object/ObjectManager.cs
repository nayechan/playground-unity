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

        
        static public ObjectManager GetOM()
        {
            return om;
        }
    }
}
