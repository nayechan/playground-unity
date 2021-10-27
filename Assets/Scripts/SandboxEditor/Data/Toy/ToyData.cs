using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor.Data
{
    [System.Serializable]
    public class ToyData
    {
        public ImageData imageData;
        public ToyComponentsDataContainer toyComponentsDataContainer;
        public ToyMiscData toyMiscData;
        public ToyRecipe toyRecipe;
        private List<ToyComponentData> toyComponentsData;

        public ToyData()
        {
            imageData = new ImageData();
            toyComponentsDataContainer = new ToyComponentsDataContainer();
            toyMiscData = new ToyMiscData();
        }

        public ToyData(ToyRecipe toyRecipe) : this()
        {
            this.toyRecipe = toyRecipe;
            toyComponentsData = toyComponentsDataContainer.GetToyComponentsData();
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
            toyComponentsData.Add(new TransformData(toyRecipe));
        }
        
        private void SetColliderData()
        {
            var colliderType = toyRecipe.toyBuildData.colliderType;
            switch (@colliderType)
            {
                case ColliderType.Circle:
                    toyComponentsData.Add(new CircleCollider2DData());
                    break;
                case ColliderType.Box:
                    toyComponentsData.Add(new BoxCollider2DData());
                    break;
                case ColliderType.None:
                    toyComponentsData.Add(new BoxCollider2DData(false));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetRigidbodyData()
        {
            toyComponentsData.Add(new Rigidbody2DData(toyRecipe));
        }

        private void SetMicsData()
        {
            toyMiscData.toyType = toyRecipe.toyBuildData.toyType;
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
            Debug.Log("Before Ser Called");
            serializedData = new List<string>();
            types = new List<string>();
            foreach(var toyComponentData in toyComponentsData)
            {
                serializedData.Add(JsonUtility.ToJson(toyComponentData));
                types.Add(toyComponentData.GetType().ToString());
            }
        }

        public void OnAfterDeserialize()
        {
            Debug.Log("After~~ called");
            for(var i = 0; i < serializedData.Count ; ++i)
            {
                var type = Type.GetType(types[i]);
                Debug.Log("TYPE : " + type.ToString());
                toyComponentsData.Add((ToyComponentData)JsonUtility.FromJson(serializedData[i], type));
                Debug.Log("serializeddata[i] " + serializedData[i].ToString());
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