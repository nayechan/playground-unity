using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace GameEditor.Data
{
    public class ToyBuilder
    {
        private GameObject newGameObject;
        private JObject gameObjectRecord;
        private ToyData toyData;

        public static GameObject CreateGameObject(JObject gameObjectRecord)
        {
            var dataManager = new ToyBuilder(gameObjectRecord);
            return dataManager.CreateGameObject();
        }

        private ToyBuilder(JObject gameObjectRecord)
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
            var toyData = new ToyData();
            SetObjectAndImageData();
            UpdateImageStorage();
            AddComponents();
        }

        private void SetObjectAndImageData()
        {
            toyData.objectData = JsonUtility.FromJson<ObjectData>((string)gameObjectRecord["ObjectData"]);
            toyData.imageData = JsonUtility.FromJson<ImageData>((string)gameObjectRecord["ImageData"]);
        }

        private void UpdateImageStorage()
        {
            var imageStorage = ImageStorage.GetSingleton();
            imageStorage.UpdateImagesDataAndSprites(toyData.imageData);
        }

        private void AddComponents()
        {
            var componentDatas = gameObjectRecordToComponentDatas(gameObjectRecord);
            foreach (var componentData in componentDatas)
            {
                toyData.AddComponentFromData(componentData);
            }
        }

        private void CreateChildrenGameObject()
        {
            foreach (JObject childRecord in gameObjectRecord["Children"])
            {                
                var childGameObject = ToyBuilder.CreateGameObject(childRecord);
                childGameObject.transform.parent = newGameObject.transform;
            }
        }

        public static GameObject CreateGameobject(ToySaver toySaver)
        {
            var newGameObject = new GameObject();
            // set object property
            toySaver.objectData.SetGameObject(newGameObject);
            // create spriteRenderer
            var spriteRendererData = new SpriteRendererData(toySaver.imageData);
            var spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
            spriteRendererData.ApplyData(spriteRenderer);
            // create rigidbody
            var rigidbody2d = newGameObject.AddComponent<Rigidbody2D>(); 
            rigidbody2d.bodyType = toySaver.objectData.isFixed?
                RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
            rigidbody2d.sharedMaterial = new PhysicsMaterial2D();
            // create collider
            switch(toySaver.objectData.colliderType)
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
            if(!toySaver.imageData.GetIsRelativeSize())
            {
                SpriteRendererData.resizeObjectScale(newGameObject, toySaver.imageData);
            }
            newGameObject.AddComponent<ToySaver>();
            return newGameObject;
        }

        // JObject로부터 List<ComponentData> 를 반환합니다.
        private List<ToyComponentData> gameObjectRecordToComponentDatas(JObject gameObjectRecord)
        {
            var componentDatas = new List<ToyComponentData>();
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