using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace GameEditor.Data
{
    public class DataManager
    {
        private GameObject newGameObject;
        private JObject gameObjectRecord;
        private DataAgent dataAgent;

        public static GameObject CreateGameObject(JObject gameObjectRecord)
        {
            var dataManager = new DataManager(gameObjectRecord);
            return dataManager.CreateGameObject();
        }

        private DataManager(JObject gameObjectRecord)
        {
            newGameObject = new GameObject();
            this.gameObjectRecord = gameObjectRecord;
        }

        private GameObject CreateGameObject()
        {
            SetGameObject();
            CreateChildrenGameObject();
            return newGameObject;
        }

        private void SetGameObject()
        {
            dataAgent = newGameObject.AddComponent<DataAgent>();
            SetObjectAndImageData();
            AppendImageStorage();
            AddComponents();
        }

        private void SetObjectAndImageData()
        {
            dataAgent.objectData = JsonUtility.FromJson<ObjectData>((string)gameObjectRecord["ObjectData"]);
            dataAgent.imageData = JsonUtility.FromJson<ImageData>((string)gameObjectRecord["ImageData"]);
        }

        private void AppendImageStorage()
        {
            var imageStorage = ImageStorage.GetSingleton();
            imageStorage.AddImageData(dataAgent.imageData);
        }

        private void AddComponents()
        {
            var componentDatas = gameObjectRecordToComponentDatas(gameObjectRecord);
            foreach (var componentData in componentDatas)
            {
                dataAgent.AddComponentFromData(componentData);
            }
        }

        private void CreateChildrenGameObject()
        {
            foreach (JObject childRecord in gameObjectRecord["Children"])
            {                
                var childGameObject = DataManager.CreateGameObject(childRecord);
                childGameObject.transform.parent = newGameObject.transform;
            }
        }

        public static GameObject CreateGameobject(DataAgent dataAgent)
        {
            var newGameObject = new GameObject();
            // set object property
            dataAgent.objectData.SetGameObject(newGameObject);
            // create spriteRenderer
            var spriteRendererData = new SpriteRendererData(dataAgent.imageData);
            var spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
            spriteRendererData.ApplyData(spriteRenderer);
            // create rigidbody
            var rigidbody2d = newGameObject.AddComponent<Rigidbody2D>(); 
            rigidbody2d.bodyType = dataAgent.objectData.isFixed?
                RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
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
            // set Scale by ImageData
            if(!dataAgent.imageData.GetIsRelativeSize())
            {
                SpriteRendererData.resizeObjectScale(newGameObject, dataAgent.imageData);
            }
            newGameObject.AddComponent<DataAgent>();
            return newGameObject;
        }

        // JObject로부터 List<ComponentData> 를 반환합니다.
        private List<ComponentData> gameObjectRecordToComponentDatas(JObject gameObjectRecord)
        {
            var componentDatas = new List<ComponentData>();
            foreach (var pair in (JObject)gameObjectRecord["Components"])
            {
                switch (pair.Key)
                {
                    case TransformData._Type:
                    {
                        componentDatas.Add(JsonUtility.FromJson<TransformData>((string)pair.Value));
                        break;
                    }
                    case BoxCollider2DData._Type:
                    {
                        componentDatas.Add(JsonUtility.FromJson<BoxCollider2DData>((string)pair.Value));
                        break;
                    }
                    case CircleCollider2DData._Type:
                    {
                        componentDatas.Add(JsonUtility.FromJson<CircleCollider2DData>((string)pair.Value));
                        break;
                    }
                    case Rigidbody2DData._Type:
                    {
                        componentDatas.Add(JsonUtility.FromJson<Rigidbody2DData>((string)pair.Value));
                        break;
                    }
                    case SpriteRendererData._Type:
                    {
                        componentDatas.Add(JsonUtility.FromJson<SpriteRendererData>((string)pair.Value));
                        break;
                    }
                }
            }

            return componentDatas;
        }

    }
}