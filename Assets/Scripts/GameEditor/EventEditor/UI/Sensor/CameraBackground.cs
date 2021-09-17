using System;
using System.Collections;
using System.Collections.Generic;
using GameEditor.Data;
using UnityEngine;
using UnityEngine.Events;

namespace GameEditor.EventEditor.UI.Sensor{

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
            Collider2D col = new BoxCollider2D();
        }

        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            var tc = TouchController.GetTID();
            switch (tc.mode)
            {
                case TouchController.TouchMode.CreateObject:
                {
                    // var om =  ObjectManager.GetOM();
                    // var data = new ObjectData();
                    // info.texturePath = 
                    break;
                }
                case TouchController.TouchMode.CamMove:
                    tc.AlarmMe(touch.fingerId, this);
                    _touchBeginPosition = touch.position;
                    _camBeginPosition = cam.transform.position;
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
            Vector3 deltaVector = 
                cam.ScreenToWorldPoint(touch.position) - cam.ScreenToWorldPoint(_touchBeginPosition);
            cam.transform.position = _camBeginPosition - deltaVector;
            m_CameraMoved.Invoke();
        }

        
    }
}