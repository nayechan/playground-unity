using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor.Data
{
    [System.Serializable]
    public class ToyData
    {
        public ObjectData objectData;
        public ImageData imageData;
        public AudioData audioData;
        public ToyComponentsData toyComponentsData;

        public ToyData()
        {
            objectData = new ObjectData();
            imageData = new ImageData();
            audioData = new AudioData();
            toyComponentsData = new ToyComponentsData();
        }


    }
    [System.Serializable]
    public class ToyComponentsData : ISerializationCallbackReceiver
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
                serializedData.Add(JsonUtility.ToJson(toyComponentData));
                types.Add(toyComponentData.GetType().ToString());
            }
        }

        public void OnAfterDeserialize()
        {
            for(int i = 0; i < serializedData.Count ; ++i)
            {
                Type type = Type.GetType(types[i]);
                toyComponentsData.Add(JsonUtility.FromJson<ToyComponentData>(serializedData[i]));
            }
        }

        public List<ToyComponentData> Get()
        {
            return toyComponentsData;
        }

    }
}