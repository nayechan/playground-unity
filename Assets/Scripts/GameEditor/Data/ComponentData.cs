using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    public abstract class ComponentData
    {
        // 인자로 받은 컴포넌트를 본 클래스가 가진 Data의 값으로 Set합니다.
        public abstract void SetComponent(Component comp);
        // 본 클래스의 Data를 받은 컴포넌트의 값으로 업데이트시켜주는 메소드입니다.
        public abstract void SetData(Component comp);
        // 인자로 받은 오브젝트에 본 클래스가 저장하는 타입의 컴포넌트를 추가한다.
        public abstract Component AddComponent(GameObject obj);
        // 컴포넌트 타입과 본 클래스의 타입이 일치하는지 확인합니다.
        public abstract bool IsCorrectType(Component comp);
    }
}