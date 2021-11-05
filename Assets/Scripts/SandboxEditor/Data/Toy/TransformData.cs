using UnityEngine;
using UnityEngine.Assertions;

namespace SandboxEditor.Data.Toy
{
    [System.Serializable]
    public class TransformData : ToyComponentData
    {
        public string name;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        
        // 인자로 받은 Component의 설정을 본 class의 Data로 설정한다.
        public override void ApplyDataToToyComponent(Component comp)
        {
            Assert.IsTrue(IsMatchedType(comp));
            var tf = (Transform)comp;
            tf.name = name;
            tf.position = position;
            tf.rotation = (Quaternion.Euler(rotation));
            tf.localScale = scale;
        }

        // Transform은 모든 오브젝트에 자동 생성되므로 Transform의 값을 
        // 본 클래스의 값으로 Set 한 뒤 Transform를 반환한다.
        public override Component AddMatchedTypeToyComponent(GameObject obj)
        {
            return obj.transform;
        }
        
        // Component의 값을 갖는 TransformData 클래스를 생성한다.
        public TransformData(Component comp)
        {
            UpdateByToyComponent(comp);
        }

        public TransformData(ToyRecipe toyRecipe)
        {
            name = toyRecipe.toyBuildData.name;
            position = rotation = Vector3.zero;
            scale = Vector3.one;
        }
        
        // 본 Class의 data를 받은 Component의 설정값으로 바꾼다.
        public sealed override ToyComponentData UpdateByToyComponent(Component comp)
        {
            Assert.IsTrue(IsMatchedType(comp));
            var tf = (Transform) comp;
            name = tf.name;
            position = tf.position;
            rotation = tf.rotation.eulerAngles;
            scale = tf.localScale;
            return this;
        }
        
        // 인자로 받은 Component의 derived 타입이 본 클래스가 담당하는
        //Component타입과 일치하는지 확인한다.
        public override bool IsMatchedType(Component comp)
        {
            return comp is Transform;
        }
    }
}