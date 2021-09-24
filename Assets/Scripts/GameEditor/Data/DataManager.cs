using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace GameEditor.Data
{
    public class DataManager
    {
        // JObject로부터 List<ComponentData> 를 반환합니다.
        public static List<ComponentData> JObjectToComponentDatas(JObject jObj)
        {
            var cds = new List<ComponentData>();
            foreach (var pair in (JObject)jObj["Components"])
            {
                switch (pair.Key)
                {
                    case TransformData._Type:
                    {
                        cds.Add(JsonUtility.FromJson<TransformData>((string)pair.Value));
                        break;
                    }
                    case BoxCollider2DData._Type:
                    {
                        cds.Add(JsonUtility.FromJson<BoxCollider2DData>((string)pair.Value));
                        break;
                    }
                    case CircleCollider2DData._Type:
                    {
                        cds.Add(JsonUtility.FromJson<CircleCollider2DData>((string)pair.Value));
                        break;
                    }
                    case Rigidbody2DData._Type:
                    {
                        cds.Add(JsonUtility.FromJson<Rigidbody2DData>((string)pair.Value));
                        break;
                    }
                }
            }

            return cds;
        }
        
        // JObject로부터 GameObject를 만든다.
        public static GameObject CreateGameObjectFromJObject(JObject jObj)
        {
            // instanceID, GameObject 쌍의 딕셔너리 생성.
            var dict = new Dictionary<int, GameObject>();
            // 오브젝트 구조 먼저 생성, DataAgent 먼저 생성한다.
            var obj = new GameObject();
            var jgObjectPair = new List<Tuple<JObject, GameObject>>();
            CreateGameObjectRecursively(obj, jObj, dict, jgObjectPair);
            // 오브젝트에 컴포넌트 추가.
            CreateComponentAll(jgObjectPair);
            return obj;
        }

        // JObject내용을 토대로 GameObject를 만들고 DataAgent를 추가한다.
        private static void CreateGameObjectRecursively
            (GameObject obj, JObject jObj, IDictionary<int, GameObject> dict,
               ICollection<Tuple<JObject, GameObject>> jgObjectPair )
        {
            jgObjectPair.Add(new Tuple<JObject, GameObject>(jObj, obj));
            var da = obj.AddComponent<DataAgent>();
            var od = da.od = JsonUtility.FromJson<ObjectData>((string)jObj["ObjectData"]);
            obj.name = od.name;
            dict.Add(od.id, obj);
            foreach (JObject childJObj in jObj["Children"])
            {
                var nObj = new GameObject
                {
                    transform =
                    {
                        parent = obj.transform
                    }
                };
                CreateGameObjectRecursively(nObj, childJObj, dict, jgObjectPair);
            }
        }

        private static void CreateComponentAll
            (IEnumerable<Tuple<JObject, GameObject>> jgObjectPair)
        {
            foreach (var (jObj, obj) in jgObjectPair)
            {
                var cds = JObjectToComponentDatas(jObj);
                foreach (var cd in cds)
                {
                    var comp = cd.AddComponent(obj);
                    cd.SetComponent(comp);  
                }
            }
        }
    }
}