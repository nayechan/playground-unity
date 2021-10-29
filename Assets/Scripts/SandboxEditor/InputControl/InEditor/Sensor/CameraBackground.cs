using System;
using SandboxEditor.Builder;
using UnityEngine;
using UnityEngine.Events;

namespace SandboxEditor.InputControl.InEditor.Sensor{

    public class CameraBackground : AbstractSensor
    {
        private Camera cam;
        private Vector2 _touchBeginPosition;
        private Vector3 _camBeginPosition;
        public UnityEvent m_CameraMoved;
        public ObjectBuilder objectBuilder;

        protected override void Start()
        {
            cam = GetComponent<Camera>();
            m_CameraMoved = new UnityEvent();
            Collider2D col = new BoxCollider2D();
        }

        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            var tc = TouchController.GetTID();
            _touchBeginPosition = touch.position;
            switch (tc.mode)
            {
                case TouchController.TouchMode.CamMove:
                    tc.AlarmMe(touch.fingerId, this);
                    _camBeginPosition = cam.transform.position;
                    break;

                case TouchController.TouchMode.CreateObject:

                    //tc.AlarmMe(touch.fingerId, this);

                    Vector3 worldPos;
                    worldPos = touch.position;
                    worldPos = Camera.main.ScreenToWorldPoint(worldPos);
                    
                    objectBuilder.GenerateObject(worldPos);
                    break;
                
                case TouchController.TouchMode.DeleteObject:
                    break;

                case TouchController.TouchMode.MoveObject:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            isRayBlock = true;
        }

        public override void CallBack(Touch touch)
        {
            var tc = TouchController.GetTID();
            // Debug.Log(touch.phase);

            Vector3 worldPos, deltaVector;
            worldPos = touch.position;
            worldPos = Camera.main.ScreenToWorldPoint(worldPos);

            switch (tc.mode)
            {
            case TouchController.TouchMode.CamMove:
                deltaVector = 
                cam.ScreenToWorldPoint(touch.position) - 
                cam.ScreenToWorldPoint(_touchBeginPosition);

                cam.transform.position = _camBeginPosition - deltaVector;

                m_CameraMoved.Invoke();

                break;

            case TouchController.TouchMode.CreateObject:
                if(objectBuilder.isSnap)
                {
                    objectBuilder.GenerateObject(worldPos);
                }
                break;
            }
            
        }

        
    }
}