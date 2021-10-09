using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace GameEditor.Data
{
    public class DataManager
    {
        private static DataManager dataManager;

        public static DataManager GetDataManager()
        {
            if(dataManager == null)
            {
                dataManager = new DataManager();
            }
            return dataManager;
        }
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
        public static GameObject CreateGameObject(JObject jObj)
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
            (GameObject targetObj, JObject objJson, IDictionary<int, GameObject> idObjDict,
               ICollection<Tuple<JObject, DataAgent>> JsonAgentPair )
        {
            var objAgent = targetObj.AddComponent<DataAgent>();
            JsonAgentPair.Add(new Tuple<JObject, DataAgent>(objJson, objAgent));
            var objectData = objAgent.objectData = JsonUtility.FromJson<ObjectData>((string)objJson["ObjectData"]);
            targetObj.name = objectData.name;
            idObjDict.Add(objectData.id, targetObj);
            foreach (JObject childJson in objJson["Children"])
            {
                var childObj = new GameObject
                {
                    transform =
                    {
                        parent = targetObj.transform
                    }
                };
                CreateGameObjectRecursively(childObj, childJson, idObjDict, JsonAgentPair);
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

        public static GameObject CreateGameobject(DataAgent dataAgent)
        {
            var newGameObject = new GameObject();
            // set object property
            dataAgent.objectData.SetGameObject(ref newGameObject);
            // create spriteRenderer
            var spriteRendererData = new SpriteRendererData(dataAgent.imageData);
            var spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
            spriteRendererData.ApplyData(spriteRenderer);
            // create rigidbody
            var rigidbody2d = newGameObject.AddComponent<Rigidbody2D>(); 
            rigidbody2d.bodyType = dataAgent.objectData.isFixed?
                RigidbodyType2D.Kinematic : RigidbodyType2D.Kinematic;
            rigidbody2d.sharedMaterial = new PhysicsMaterial2D();
            // create collider
            switch(dataAgent.objectData.colliderType)
            {
                case ColliderType.Circle:
                {
                    newGameObject.AddComponent<CircleCollider2D>();
                    break;   
                }
                case ColliderType.Box:
                {
                    newGameObject.AddComponent<BoxCollider2D>();
                    break;   
                }
                case ColliderType.None:
                {
                    break;   
                }
            }
            // create audioSource
            return newGameObject;
        }



    }
}