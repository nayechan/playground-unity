using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditorDataManager
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
            foreach(KeyValuePair<K,V> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            if(keys.Count != values.Count)
                throw new System.Exception("Key or value is not supported type.");
            for(int i=0;i<keys.Count;++i)
                this.Add(keys[i], values[i]);
        }
    }
    public class MapEditorData{
        public class TileType{
            private string spritePath;
            private string name;
            TileType(string path, string name)
            {
                spritePath = path;
                this.name = name;
            }
        }
        public class TileData{
            private Vector3 position;
            private System.Guid tileGuid;
            TileData(Vector3 position, System.Guid guid)
            {
                this.position = position;
                tileGuid = guid;
            }
        }
        SerializableDictionary<System.Guid, TileType> tileTypes;
        List<TileData> tileDatas;
    }
    
    [Serializable]
    public class ObjectEditorData{
        [Serializable]
        public class ObjectType{
            [SerializeField] private string[] sprites;
            [SerializeField] private string objectName;
            [SerializeField] private string objectType;
            [SerializeField] private float width, height;
            [SerializeField] private System.Guid guid;
            public ObjectType(
                string[] sprites, string objectName, string objectType,
                float width, float height, System.Guid guid    
            )
            {
                this.sprites = sprites;
                this.objectName = objectName;
                this.objectType = objectType;
                this.width = width;
                this.height = height;
                this.guid = guid;
            }
            public string[] GetSprites(){return sprites;}
            public string GetObjectName(){return objectName;}
            public string GetObjectType(){return objectType;}
            public float GetWidth(){return width;}
            public float GetHeight(){return height;}
            public System.Guid GetGuid(){return guid;}
        }

        [Serializable]
        public class ObjectData{
            [SerializeField] Vector3 _position;
            [SerializeField] System.Guid _objectGuid;
            public ObjectData(Vector3 position, System.Guid objectGuid)
            {
                _position = position;
                _objectGuid = objectGuid;
            }
            public Vector3 GetPosition(){return _position;}
            public Guid GetGuid(){return _objectGuid;}
        }
        [SerializeField] SerializableDictionary<System.Guid, ObjectType> objectTypes;
        [SerializeField] List<ObjectData> objectDatas;
        public ObjectEditorData()
        {
            objectTypes = new SerializableDictionary<Guid, ObjectType>();
            objectDatas = new List<ObjectData>();
        }
        public void AddObjectType(ObjectType data)
        {
            objectTypes.Add(data.GetGuid(),data);
        }
        public void AddObjectData(ObjectData data)
        {
            objectDatas.Add(data);
        }
    }
    public class EventEditorData{

    }
    
    [Serializable]
    public class JsonData{
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
    }

    public class A{
        [SerializeField] int a;
        [SerializeField] B _b;

        public A(int a, int b, int c, int d)
        {
            this.a = a;
            _b = new B(b,c,d);
        }

    }

    [Serializable]
    public class B{
        [SerializeField] int b;
        [SerializeField] Dictionary<int,int> c;
        public B(int b, int c, int d)
        {
            this.b = b;
            this.c = new Dictionary<int, int>();
            this.c.Add(c,d);
            this.c.Add(d,c);
        }
    }

    public class GameEditorDataManager : MonoBehaviour
    {
        JsonData jsonData = new JsonData();

        private void Start() {
            Debug.Log(JsonUtility.ToJson(new A(1,2,3,4)));
        }

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
        }
    }
}

