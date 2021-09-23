using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace GameEditor.Data
{
    public class DataManager : MonoBehaviour
    {
        // JObject로부터 List<ComponentData> 를 반환합니다.
        public List<ComponentData> JObjectToComponentDatas(JObject jObj)
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
        // public GameObject CreateGameObjectFromJObject(JObject jObj)
        // {
        //     var dict = new Dictionary<int, GameObject>();
        // 오브젝트 구조 먼저 생성 후, 컴포넌트 추가. 우선 오브젝트와 DataAgent 먼저 생성한다. 이후 SetComponent
        //     var obj = new GameObject(); 
        //     var da = obj.AddComponent<DataAgent>();
        //     da.od = JsonUtility.FromJson<ObjectData>((string)jObj["ObjectData"]);
        //     obj.name = da.od.name;
        //
        //     var cds = JObjectToComponentDatas(jObj);
        //     foreach(var cd in cds)
        //     {
        //         var comp = cd.AddComponent(obj);
        //         cd.SetComponent(comp);  
        //     }
        //
        //     var children = (JArray) jObj["Children"];
        //     foreach (JObject child in children)
        //     {
        //         
        //     }
        // }
    }
}