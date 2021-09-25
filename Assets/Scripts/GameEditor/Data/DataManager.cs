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
                    case SpriteRendererData._Type:
                    {
                        cds.Add(JsonUtility.FromJson<SpriteRendererData>((string)pair.Value));
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
            var jaObjectPair = new List<Tuple<JObject, DataAgent>>();
            CreateGameObjectRecursively(obj, jObj, dict, jaObjectPair);
            // 오브젝트에 컴포넌트 추가.
            CreateComponentAll(jaObjectPair);
            return obj;
        }

        // JObject내용을 토대로 GameObject를 만들고 DataAgent를 추가한다.
        private static void CreateGameObjectRecursively
            (GameObject obj, JObject jObj, IDictionary<int, GameObject> dict,
               ICollection<Tuple<JObject, DataAgent>> jaObjectPair )
        {
            var da = obj.AddComponent<DataAgent>();
            jaObjectPair.Add(new Tuple<JObject, DataAgent>(jObj, da));
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
                CreateGameObjectRecursively(nObj, childJObj, dict, jaObjectPair);
            }
        }

        private static void CreateComponentAll
            (IEnumerable<Tuple<JObject, DataAgent>> jaObjectPair)
        {
            foreach (var (jObj, da) in jaObjectPair)
            {
                var cds = JObjectToComponentDatas(jObj);
                foreach (var cd in cds)
                {
                    da.AddComponentFromData(cd);
                }
            }
        }
    }
}