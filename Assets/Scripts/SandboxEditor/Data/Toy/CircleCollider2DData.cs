﻿using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace SandboxEditor.Data.Toy
{
    [Serializable]
    public class CircleCollider2DData : ToyComponentData
    {
        public bool enabled, isTrigger;
        public float colRadius;
        public Vector2 offset;
        public PhysicsMaterial2DData pm2dd;
        
        // 인자로 받은 Component의 설정을 본 class의 Data로 설정한다.
        public override void ApplyDataToToyComponent(Component comp)
        {
            Assert.IsTrue(IsMatchedType(comp));
            var cir2d = (CircleCollider2D)comp;
            cir2d.radius = colRadius;
            cir2d.enabled = enabled;
            cir2d.isTrigger = isTrigger;
            cir2d.offset = offset;
            pm2dd?.SetComponent(cir2d.sharedMaterial); 
        }

        // 인자로 받은 GameObject에 CircleCollider2D 컴포넌트를 추가하고
        //해당 컴포넌트를 반환한다.
        public override Component AddMatchedTypeToyComponent(GameObject obj)
        {
            var cir2d = obj.AddComponent<CircleCollider2D>();
            cir2d.sharedMaterial = new PhysicsMaterial2D();
            return cir2d;
        }

        // Component의 값을 갖는 CircleCollider2DDate 클래스를 생성한다.
        public CircleCollider2DData(Component comp)
        {
            UpdateByToyComponent(comp);
        }

        public CircleCollider2DData()
        {
            colRadius = 0.5f;
            offset = Vector2.zero;
            enabled = true;
            isTrigger = false;
        }

        // 본 Class의 data를 받은 Component의 설정값으로 바꾼다.
        public sealed override ToyComponentData UpdateByToyComponent(Component comp)
        {
            Assert.IsTrue(IsMatchedType(comp));
            var cir2d = (CircleCollider2D)comp;
            colRadius = cir2d.radius;
            offset = cir2d.offset;
            enabled = cir2d.enabled;
            pm2dd = new PhysicsMaterial2DData(cir2d.sharedMaterial);
            return this;
        }
        
        // 인자로 받은 Component의 derived 타입이 본 클래스가 담당하는
        //Component타입과 일치하는지 확인한다.
        public override bool IsMatchedType(Component comp)
        {
            return comp is CircleCollider2D;
        }
    }
}