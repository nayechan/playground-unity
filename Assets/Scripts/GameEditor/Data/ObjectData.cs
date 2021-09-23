using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    // 오브젝트의 이름, 인스턴스 ID등의 정보를 저장하는 클래스입니다.
    [System.Serializable]
    public class ObjectData 
    {
        public string name;
        public int id;
        public ObjectData()
        {
            name = "NoNamed";
            id = 0;
        }

        public void SetData(GameObject obj)
        {
            obj.name = name;
        }

    }
}