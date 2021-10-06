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
        public string Name {get; set;}
        public int Id{get; set;}
        public ToyType ToyType{get; set;}
        public ColliderType ColliderType{get; set;}
        public ObjectData()
        {
            Name = "NoNamed";
            Id = 0;
            ColliderType = ColliderType.None;
            ToyType = ToyType.Friend;
        }

        public void SetData(GameObject obj)
        {
            obj.name = Name;
        }

    }
}