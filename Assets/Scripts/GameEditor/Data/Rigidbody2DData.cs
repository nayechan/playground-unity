using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    [System.Serializable]
    public class Rigidbody2DData : ComponentData
    {
        public bool movable;
        public float mass;
        public float gravityScale;
        public float linearDrag;
        public float angularDrag;
        public PhysicsMaterial2DData pm2dd;
        
        public override string Type => _Type;
        public const string _Type = "Rigidbody2DData";
        
        // 인자로 받은 Component의 설정을 본 class의 Data로 설정한다.
        public override void ApplyData(Component comp)
        {
            Assert.IsTrue(IsCorrectType(comp));
            var rb2d = (Rigidbody2D)comp;
            rb2d.bodyType = movable ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic; 
            rb2d.mass = mass;
            rb2d.gravityScale = gravityScale;
            rb2d.drag = linearDrag;
            rb2d.angularDrag = angularDrag;
            pm2dd.SetComponent(rb2d.sharedMaterial); 
        }
        
        // 인자로 받은 GameObject에 CircleCollider2D 컴포넌트를 추가하고
        //해당 컴포넌트를 반환한다.
        public override Component AddComponent(GameObject obj)
        {
            var rb2d = obj.AddComponent<Rigidbody2D>();
            rb2d.sharedMaterial = new PhysicsMaterial2D();
            return rb2d;
        }

        // Component의 값을 갖는 Rigidbody2DData 클래스를 생성한다.
        public Rigidbody2DData(Component comp)
        {
            SetData(comp);
        }
        
        // 본 Class의 data를 받은 Component의 설정값으로 바꾼다.
        public sealed override void SetData(Component comp)
        {
            Assert.IsTrue(IsCorrectType(comp));
            var rb2d = (Rigidbody2D)comp;
            movable = rb2d.bodyType == RigidbodyType2D.Dynamic ? true : false;
            mass = rb2d.mass;
            gravityScale = rb2d.gravityScale;
            linearDrag = rb2d.drag;
            angularDrag = rb2d.angularDrag;
            pm2dd = new PhysicsMaterial2DData(rb2d.sharedMaterial);
        }

        // 인자로 받은 Component의 derived 타입이 본 클래스가 담당하는
        //Component타입과 일치하는지 확인한다.
        public override bool IsCorrectType(Component comp)
        {
            return comp is Rigidbody2D;
        }
    }
    
}