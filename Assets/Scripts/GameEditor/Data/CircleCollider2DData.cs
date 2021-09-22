using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    [System.Serializable]
    public class CircleCollider2DData : ComponentData
    {
        public bool collidable, isTrigger;
        public float colRadius;
        public Vector2 offset;
        public PhysicsMaterial2DData pm2dd;
        
        // 인자로 받은 Component의 설정을 본 class의 Data로 설정한다.
        public override void SetComponent(Component comp)
        {
            Assert.IsTrue(IsCorrectType(comp));
            var cir2d = (CircleCollider2D)comp;
            cir2d.radius = colRadius;
            cir2d.enabled = collidable;
            cir2d.offset = offset;
            pm2dd.SetComponent(cir2d.sharedMaterial); 
        }

        // 인자로 받은 GameObject에 CircleCollider2D 컴포넌트를 추가하고
        //해당 컴포넌트를 반환한다.
        public override Component AddComponent(GameObject obj)
        {
            var cir2d = obj.AddComponent<CircleCollider2D>();
            cir2d.sharedMaterial = new PhysicsMaterial2D();
            return cir2d;
        }

        // Component의 값을 갖는 CircleCollider2DDate 클래스를 생성한다.
        public CircleCollider2DData(Component comp)
        {
            SetData(comp);
        }

        // 본 Class의 data를 받은 Component의 설정값으로 바꾼다.
        public sealed override void SetData(Component comp)
        {
            Assert.IsTrue(IsCorrectType(comp));
            var cir2d = (CircleCollider2D)comp;
            colRadius = cir2d.radius;
            collidable = cir2d.enabled;
            isTrigger = cir2d.isTrigger;
            offset = cir2d.offset;
            pm2dd = new PhysicsMaterial2DData(cir2d.sharedMaterial);
        }
        
        // 인자로 받은 Component의 derived 타입이 본 클래스가 담당하는
        //Component타입과 일치하는지 확인한다.
        public override bool IsCorrectType(Component comp)
        {
            return comp is CircleCollider2D;
        }
    }
}