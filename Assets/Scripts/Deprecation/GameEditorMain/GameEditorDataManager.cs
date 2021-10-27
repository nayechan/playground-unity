using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEditor.GameEditorMain
{
    [Serializable]
    public class SerializableDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<K> keys = new List<K>();

        [SerializeField]
        private List<V> values = new List<V>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<K, V> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            if (keys.Count != values.Count)
                throw new System.Exception("Key or value is not supported type.");
            for (int i = 0; i < keys.Count; ++i)
                this.Add(keys[i], values[i]);
        }
    }
    public class MapEditorData
    {
        public class TileType
        {
            private string spritePath;
            private string name;
            TileType(string path, string name)
            {
                spritePath = path;
                this.name = name;
            }
        }
        public class TileData
        {
            private Vector3 position;
            private string tileGuid;
            TileData(Vector3 position, string guid)
            {
                this.position = position;
                tileGuid = guid;
            }
        }
        SerializableDictionary<string, TileType> tileTypes;
        List<TileData> tileDatas;
    }

    [Serializable]
    public class ObjectType
    {
        [SerializeField] private string[] sprites;
        [SerializeField] private string objectName;
        [SerializeField] private string objectType;
        [SerializeField] private float width, height;
        [SerializeField] private string guid;
        public ObjectType(
            string[] sprites, string objectName, string objectType,
            float width, float height, string guid
        )
        {
            this.sprites = sprites;
            this.objectName = objectName;
            this.objectType = objectType;
            this.width = width;
            this.height = height;
            this.guid = guid;
        }
        public string[] GetSprites() { return sprites; }
        public string GetObjectName() { return objectName; }
        public string GetObjectType() { return objectType; }
        public float GetWidth() { return width; }
        public float GetHeight() { return height; }
        public string GetGuid() { return guid; }
    }

    [Serializable]
    public class ObjectData
    {
        [SerializeField] Vector3 _position;
        [SerializeField] string  _objectGuid;
        public ObjectData(Vector3 position, string objectGuid)
        {
            _position = position;
            _objectGuid = objectGuid;
        }
        public Vector3 GetPosition() { return _position; }
        public string GetGuid() { return _objectGuid; }
    }

    [Serializable]
    public class ObjectEditorData
    {
        [SerializeField] SerializableDictionary<string, ObjectType> objectTypes;
        [SerializeField] List<ObjectData> objectDatas;
        public ObjectEditorData()
        {
            objectTypes = new SerializableDictionary<string, ObjectType>();
            objectDatas = new List<ObjectData>();
        }
        public void AddObjectType(ObjectType data)
        {
            objectTypes.Add(data.GetGuid(), data);
            Debug.Log(objectTypes.Count);
        }
        public void AddObjectData(ObjectData data)
        {
            objectDatas.Add(data);
            Debug.Log(objectDatas.Count);
        }
        public SerializableDictionary<string, ObjectType> GetObjectTypes()
        {
            return objectTypes;
        }

        public List<ObjectData> GetObjectDatas()
        {
            return objectDatas;
        }
    }
    public class EventEditorData
    {

    }

    [Serializable]
    public class JsonData
    {
        [SerializeField] MapEditorData mapEditorData;
        [SerializeField] ObjectEditorData objectEditorData;
        public void setMapEditorData(MapEditorData data)
        {
            mapEditorData = data;
        }
        public void setObjectEditorData(ObjectEditorData data)
        {
            objectEditorData = data;
        }
        public void printData()
        {
            if(objectEditorData == null) return;
            SerializableDictionary<string, ObjectType> type = objectEditorData.GetObjectTypes();
            List<ObjectData> data = objectEditorData.GetObjectDatas();

            foreach(KeyValuePair<string, ObjectType> pair in type)
            {
                Debug.Log("Object Type Pair : "+pair.Key+" "+JsonUtility.ToJson(pair.Value));
            }
            foreach(ObjectData od in data)
            {
                Debug.Log("Object Data : "+od.GetGuid()+" "+od.GetPosition());
            }
        }
    }

    public class GameEditorDataManager : MonoBehaviour
    {
        JsonData jsonData = new JsonData();

        public void setMapEditorData(MapEditorData data)
        {
            jsonData.setMapEditorData(data);
        }

        public void setObjectEditorData(ObjectEditorData data)
        {
            jsonData.setObjectEditorData(data);
        }

        public void printJsonString()
        {
            Debug.Log(JsonUtility.ToJson(jsonData));
            //jsonData.printData();
        }
    }
}

