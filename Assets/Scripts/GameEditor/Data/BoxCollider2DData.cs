using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace GameEditor.Data
{
    [System.Serializable]
    public class BoxCollider2DData : ToyComponentData
    {
        public bool collidable, isTrigger;
        public Vector2 size;
        public Vector2 offset;
        public PhysicsMaterial2DData pm2dd;

        public override string Type => _Type;
        public const string _Type = "BoxCollider2DData";
        
        // 인자로 받은 Component의 설정을 본 class의 Data로 설정한다.
        public override void ApplyData(Component comp)
        {
            Assert.IsTrue(IsCorrectType(comp));
            var box2d = (BoxCollider2D) comp;
            box2d.size = size;
            box2d.offset = offset;
            box2d.enabled = collidable;
            pm2dd.SetComponent(box2d.sharedMaterial); 
        }

        // Component의 값을 갖는 BoxCollider2DData 클래스를 생성한다.
        public BoxCollider2DData(Component comp)
        {
            UpdateByToyComponent(comp);
        }

        // 인자로 받은 GameObject에 BoxCollider2D 컴포넌트를 추가하고
        //해당 컴포넌트를 반환한다.
        public override Component AddMatchedToyComponent(GameObject gameObject)
        {
            var box2d = gameObject.AddComponent<BoxCollider2D>();
            box2d.sharedMaterial = new PhysicsMaterial2D();
            return box2d;
        }
        
        // 본 Class의 data를 받은 Component의 설정값으로 바꾼다.
        public sealed override void UpdateByToyComponent(Component comp)
        {
            Assert.IsTrue(IsCorrectType(comp));
            var box2d = (BoxCollider2D)comp;
            size = box2d.size;
            collidable = box2d.enabled;
            isTrigger = box2d.isTrigger;
            offset = box2d.offset;
            pm2dd = new PhysicsMaterial2DData(box2d.sharedMaterial);
        }

        // 인자로 받은 Component의 derived 타입이 본 클래스가 담당하는
        //Component타입과 일치하는지 확인한다.
        public override bool IsCorrectType(Component comp)
        {
            return comp is BoxCollider2D;
        }
    }
}