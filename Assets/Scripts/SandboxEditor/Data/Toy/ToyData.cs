using System;
using System.Collections.Generic;
using SandboxEditor.Data.Resource;
using SandboxEditor.Data.Storage;
using UnityEngine;

namespace SandboxEditor.Data.Toy
{
    [Serializable]
    public class ToyData
    {
        public ImageData imageData;
        public ToyComponentsDataContainer toyComponentsDataContainer;
        public ToyMiscData toyMiscData;
        public ToyRecipe toyRecipe;
        public List<ToyData> childToysData;
        public int gameObjectInstanceID;

        public ToyData()
        {
            imageData = new ImageData();
            toyComponentsDataContainer = new ToyComponentsDataContainer();
            toyMiscData = new ToyMiscData();
            childToysData = new List<ToyData>();
        }

        public ToyData(ToyRecipe toyRecipe) : this()
        {
            this.toyRecipe = toyRecipe;
            SetImageData();
            SetComponentsData();
            SetMicsData();
        }

        private void SetImageData()
        {
            imageData = toyRecipe.imageData;
        }

        private void SetComponentsData()
        {
            SetTransformData();
            SetColliderData();
            SetRigidbodyData();
        }

        private void SetTransformData()
        {
            toyComponentsDataContainer.Add(new TransformData(toyRecipe));
        }
        
        private void SetColliderData()
        {
            var colliderType = toyRecipe.toyBuildData.colliderType;
            switch (@colliderType)
            {
                case ColliderType.Circle:
                    toyComponentsDataContainer.Add(new CircleCollider2DData());
                    break;
                case ColliderType.Box:
                    toyComponentsDataContainer.Add(new BoxCollider2DData());
                    break;
                case ColliderType.None:
                    toyComponentsDataContainer.Add(new BoxCollider2DData(false));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetRigidbodyData()
        {
            toyComponentsDataContainer.Add(new Rigidbody2DData(toyRecipe));
        }

        private void SetMicsData()
        {
            toyMiscData.toyType = toyRecipe.toyBuildData.toyType;
        }
        
        // 외부 호출용 메서드
        
        public Vector2 GetToySpriteBoundSize()
        {
            return ImageStorage.GetSprites(imageData)[0].bounds.size;
        }

        public Vector3 GetToySpriteBoundSize3D()
        {
            var size2D = ImageStorage.GetSprites(imageData)[0].bounds.size;
            return new Vector3(size2D.x, size2D.y, 0.1f);
        }

    }
    [System.Serializable]
    public class ToyComponentsDataContainer : ISerializationCallbackReceiver
    {
        [NonSerialized]
        public List<ToyComponentData> toyComponentsData = new List<ToyComponentData>();
        [SerializeField]
        public List<string> serializedData;
        [SerializeField]
        public List<string> types;

        public void Add(ToyComponentData toyComponentData)
        {
            toyComponentsData.Add(toyComponentData);
        }
        
        public void OnBeforeSerialize()
        {
            serializedData = new List<string>();
            types = new List<string>();
            foreach(var toyComponentData in toyComponentsData)
            {
                serializedData.Add(JsonUtility.ToJson(toyComponentData, true));
                types.Add(toyComponentData.GetType().ToString());
            }
        }

        public void OnAfterDeserialize()
        {
            for(var i = 0; i < serializedData.Count ; ++i)
            {
                var type = Type.GetType(types[i]);
                toyComponentsData.Add((ToyComponentData)JsonUtility.FromJson(serializedData[i], type));
            }
        }

        public List<ToyComponentData> GetToyComponentsData()
        {
            return toyComponentsData;
        }

    }

    [Serializable]
    public class ToyMiscData
    {
        public ToyType toyType;
    }
    
}