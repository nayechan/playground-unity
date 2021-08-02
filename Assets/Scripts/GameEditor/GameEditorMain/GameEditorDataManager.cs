using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditorDataManager
{
    [Serializable]
    public class SerializableDictionary<K, V>
    {
        public Dictionary<K,V> dictionary;
        public SerializableDictionary()
        {
            dictionary = new Dictionary<K, V>();
        }
    }
    public class MapEditorData{
        public class TileType{
            string spritePath;
            string name;
        }
        public class TileData{
            Vector3 position;
            System.Guid tileGuid;
        }
        SerializableDictionary<System.Guid, TileType> tileTypes;
        List<TileData> tileDatas;
    }
    public class ObjectEditorData{
        public class ObjectType{
            private Sprite[] sprites;
            private string objectName;
            private string objectType;
            private float width, height;
            private System.Guid guid;
        }
        public class ObjectData{
            Vector3 _position;
            System.Guid _objectGuid;
            public ObjectData(Vector3 position, System.Guid objectGuid)
            {
                _position = position;
                _objectGuid = objectGuid;
            }
        }
        SerializableDictionary<System.Guid, ObjectPrimitiveData> objectTypes;
        List<ObjectData> objectDatas;
        public void AddObjectType(ObjectPrimitiveData data)
        {
            objectTypes.dictionary.Add(data.GetGuid(),data);
        }
        public void AddObjectData(ObjectData data)
        {
            objectDatas.Add(data);
        }
    }
    public class EventEditorData{

    }
    public class JsonData{
        MapEditorData mapEditorData;
        ObjectEditorData objectEditorData;
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

