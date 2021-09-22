using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    [System.Serializable]
    public class TransformData : ComponentData
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        
        // 인자로 받은 Component의 설정을 본 class의 Data로 설정한다.
        public override void SetComponent(Component comp)
        {
            Assert.IsTrue(IsCorrectType(comp));
            var tf = (Transform)comp;
            tf.position = position;
            tf.rotation = (Quaternion.Euler(rotation));
            tf.localScale = scale;
        }

        // Transform은 모든 오브젝트에 자동 생성되므로 해당 오브젝트
        //의 Transform만 반환한다.
        public override Component AddComponent(GameObject obj)
        {
            return obj.transform;
        }
        
        // Component의 값을 갖는 TransformData 클래스를 생성한다.
        public TransformData(Component comp)
        {
            SetData(comp);
        }
        
        // 본 Class의 data를 받은 Component의 설정값으로 바꾼다.
        public sealed override void SetData(Component comp)
        {
            Assert.IsTrue(IsCorrectType(comp));
            var tf = (Transform) comp;
            position = tf.position;
            rotation = tf.rotation.eulerAngles;
            scale = tf.localScale;
        }
        
        // 인자로 받은 Component의 derived 타입이 본 클래스가 담당하는
        //Component타입과 일치하는지 확인한다.
        public override bool IsCorrectType(Component comp)
        {
            return comp is Transform;
        }
    }
}