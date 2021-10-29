using UnityEngine;
using UnityEngine.Assertions;

namespace SandboxEditor.Data.Toy
{
    [System.Serializable]
    public class BoxCollider2DData : ToyComponentData
    {
        public bool enabled, isTrigger;
        public Vector2 size;
        public Vector2 offset;
        public PhysicsMaterial2DData pm2dd;

        // 인자로 받은 Component의 설정을 본 class의 Data로 설정한다.
        public override void ApplyDataToToyComponent(Component component)
        {
            Assert.IsTrue(IsMatchedType(component));
            var box2d = (BoxCollider2D) component;
            box2d.size = size;
            box2d.offset = offset;
            box2d.enabled = enabled;
            box2d.autoTiling = true;
            pm2dd?.SetComponent(box2d.sharedMaterial); 
        }

        // Component의 값을 갖는 BoxCollider2DData 클래스를 생성한다.
        public BoxCollider2DData(Component component)
        {
            UpdateByToyComponent(component);
        }

        // 본 Class의 data를 받은 Component의 설정값으로 바꾼다.
        public sealed override ToyComponentData UpdateByToyComponent(Component comp)
        {
            Assert.IsTrue(IsMatchedType(comp));
            var box2d = (BoxCollider2D)comp;
            size = box2d.size;
            enabled = box2d.enabled;
            isTrigger = box2d.isTrigger;
            offset = box2d.offset;
            pm2dd = new PhysicsMaterial2DData(box2d.sharedMaterial);
            return this;
        }

        public BoxCollider2DData(bool isEnabled = true)
        {
            size = Vector2.one;
            offset = Vector2.zero;
            enabled = true;
            isTrigger = isEnabled;
        }
        // 인자로 받은 GameObject에 BoxCollider2D 컴포넌트를 추가하고
        //해당 컴포넌트를 반환한다.
        public override Component AddMatchedTypeToyComponent(GameObject gameObject)
        {
            var box2d = gameObject.AddComponent<BoxCollider2D>();
            box2d.sharedMaterial = new PhysicsMaterial2D();
            return box2d;
        }
        

        // 인자로 받은 Component의 derived 타입이 본 클래스가 담당하는
        //Component타입과 일치하는지 확인한다.
        public override bool IsMatchedType(Component comp)
        {
            return comp is BoxCollider2D;
        }
    }
}