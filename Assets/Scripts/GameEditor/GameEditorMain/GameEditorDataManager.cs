using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditorDataManager
{
    public class MapEditorData{
        public class TileType{
            Sprite sprite;
            string name;
        }
        public class TileData{
            Vector3 position;
            System.Guid tileGuid;
        }
        Dictionary<System.Guid, TileType> tileTypes;
        List<TileData> tileDatas;
    }
    public class ObjectEditorData{
        public class ObjectData{
            Vector3 _position;
            System.Guid _objectGuid;
            public ObjectData(Vector3 position, System.Guid objectGuid)
            {
                _position = position;
                _objectGuid = objectGuid;
            }
        }
        Dictionary<System.Guid, ObjectPrimitiveData> objectTypes;
        List<ObjectData> objectDatas;
        public void AddObjectType(ObjectPrimitiveData data)
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
    public class GameEditorDataManager : MonoBehaviour
    {
        MapEditorData _mapEditorData;
        ObjectEditorData _objectEditorData;
        public void SaveMapEditorData(MapEditorData mapEditorData)
        {
            _mapEditorData = mapEditorData;
        }
        public void SaveObjectEditorData(ObjectEditorData objectEditorData)
        {
            _objectEditorData = objectEditorData;
        }
        public MapEditorData LoadMapEditorData()
        {
            return _mapEditorData;
        }
        public ObjectEditorData LoadObjectEditorData()
        {
            return _objectEditorData;
        }
        public void printJsonString()
        {
            Debug.Log(JsonUtility.ToJson(this));
        }
    }
}

