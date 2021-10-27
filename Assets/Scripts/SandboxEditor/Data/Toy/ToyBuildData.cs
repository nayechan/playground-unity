using System;
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
    [Serializable]
    public class ToyBuildData 
    {
        public string name;
        public ColliderType colliderType;
        public ToyType toyType;
        public bool isFixed;

        public ToyBuildData()
        {
            name = "Unnamed";
            colliderType = ColliderType.None;
            toyType = ToyType.Friend;
            isFixed = true;
        }
    }
}