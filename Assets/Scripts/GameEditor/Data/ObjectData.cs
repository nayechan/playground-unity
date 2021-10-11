using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    public enum ColliderType
    {
        Circle,
        Box,
        None
    }
    public enum ToyType
    {
        Friend = 1,
        Enemy = 2,
        Neutral = 4
    }
    // 오브젝트의 이름, 인스턴스 ID등의 정보를 저장하는 클래스입니다.
    [System.Serializable]
    public class ObjectData 
    {
        public string name;
        public int id;
        public ColliderType colliderType;
        public ToyType toyType;
        public bool isFixed;
        public int imageDataHashCode;
        public ObjectData()
        {
            name = "Unnamed";
            colliderType = ColliderType.None;
            toyType = ToyType.Friend;
            isFixed = true;
            imageDataHashCode = 0;
        }

        public GameObject SetGameObject(GameObject gameObject)
        {
            gameObject.name = name;
            return gameObject;
        }
    }
}