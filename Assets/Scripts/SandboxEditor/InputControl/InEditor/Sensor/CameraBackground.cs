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

        protected override void Start()
        {
            cam = GetComponent<Camera>();
            m_CameraMoved = new UnityEvent();
        }

        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            var tc = TouchInEditor.GetTID();
            Vector3 worldPos;
            _touchBeginPosition = touch.position;
            switch (TouchInEditor.Mode)
            {
                case TouchMode.CamMove:
                    tc._AlarmMe(touch.fingerId, this);
                    _camBeginPosition = cam.transform.position;
                    break;

                case TouchMode.CreateObject:
                    worldPos = touch.position;
                    worldPos = Camera.main.ScreenToWorldPoint(worldPos);
                    ObjectBuilder.BuildAndPlaceToy(worldPos);
                    break;
                
                case TouchMode.DeleteObject:
                    tc._AlarmMe(touch.fingerId, this);
                    _camBeginPosition = cam.transform.position;
                    break;
                case TouchMode.CreateBlock:
                    worldPos = touch.position;
                    worldPos = Camera.main.ScreenToWorldPoint(worldPos);
                    BlockBuilder.CreateSelectedBlockAndAddOnStorage(worldPos);
                    break;

                case TouchMode.MoveObject:
                    tc._AlarmMe(touch.fingerId, this);
                    _camBeginPosition = cam.transform.position;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            isRayBlock = true;
        }

        public override void CallBack(Touch touch)
        {
            var deltaVector = cam.ScreenToWorldPoint(touch.position) - 
                              cam.ScreenToWorldPoint(_touchBeginPosition);
            cam.transform.position = _camBeginPosition - deltaVector;
            m_CameraMoved.Invoke();
        }

        
    }
}