using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    public abstract class ToyComponentData
    {
        private static HashSet<Type> supportedType = 
            new HashSet<Type>()
            {
                typeof(BoxCollider2D),
                typeof(CircleCollider2D),
                typeof(Rigidbody2D), 
                typeof(Transform)
            };

        public abstract string Type { get; }

        // 인자로 받은 컴포넌트를 본 클래스가 가진 Data의 값으로 Set합니다.
        public abstract void ApplyData(Component comp);

        // 본 클래스의 Data를 받은 컴포넌트의 값으로 업데이트시켜주는 메소드입니다.
        // 추후 인자로 Dictionary<int, GameObject> 를 추가하여 오브젝트간 연결을 돕는다.
        public abstract void UpdateByToyComponent(Component comp);

        public Component AddDataAppliedToyComponent(GameObject gameObject)
        {
            var component = AddMatchedToyComponent(gameObject);
            ApplyData(component);
            return component;
        }
        // 인자로 받은 오브젝트에 본 클래스가 저장하는 타입의 컴포넌트를 추가한다.
        public abstract Component AddMatchedToyComponent(GameObject obj);

        // 컴포넌트 타입과 본 클래스의 타입이 일치하는지 확인합니다.
        public abstract bool IsCorrectType(Component comp);
        public static bool IsSupportedType(Component component)
        {
            return supportedType.Contains(component.GetType());
        }

        // 인자로 받은 Component타입에 맞는 ComponentData를 생성합니다.
        public static ToyComponentData CreateComponentData(Component comp)
        {
            switch (comp)
            {
                case BoxCollider2D box2d:
                    return new BoxCollider2DData(comp);
                case CircleCollider2D cir2d:
                    return new CircleCollider2DData(comp);
                case Rigidbody2D rb2d:
                    return new Rigidbody2DData(comp);
                case Transform tf:
                    return new TransformData(comp);
                default:
                    return null;
            }
        }

    }
}