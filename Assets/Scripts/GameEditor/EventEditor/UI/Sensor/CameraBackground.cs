using System.Collections;
using System.Collections.Generic;
using GameEditor.Info;
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
            TouchController tc = TouchController.GetTID();
            if(tc.mode == TouchController.TouchMode.CreateObject)
            {
                ObjectManager om =  ObjectManager.GetOM();
                ObjectData info = new ObjectData();
                // info.texturePath = 
            }
            if(tc.mode == TouchController.TouchMode.CamMove)
            {
                tc.AlarmMe(touch.fingerId, this);
                _touchBeginPosition = touch.position;
                _camBeginPosition = cam.transform.position;
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