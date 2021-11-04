using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class AbstractSensor : MonoBehaviour
    {
        public delegate void Del();
        protected virtual void Start(){
        }
        public virtual void OnTouchBegan(Touch touch, out bool isRayBlock){
            isRayBlock = true;
        }
        public virtual void OnTouchEnded(Touch touch, out bool isRayBlock){
            isRayBlock = true;
        }
        public virtual void OnTouchMoved(Touch touch, out bool isRayBlock){
            isRayBlock = true;
        }
        public virtual void OnTouchCanceled(Touch touch, out bool isRayBlock){
            isRayBlock = true;
        }
        public virtual void OnTouchStationary(Touch touch, out bool isRayBlock){
            isRayBlock = true;
        }

        public virtual void CallBack(Touch touch){
        }
    }
}
