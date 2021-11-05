using System;
using GameEditor.EventEditor.Controller;
using SandboxEditor.Builder;
using Unity.VisualScripting;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class ToyBodySensor : AbstractSensor
    {
        private Camera cam;
        private ObjectBuilder objectBuilder;

        protected override void Start()
        {
            cam = Camera.main;
            objectBuilder = GameObject.Find("ObjectBuilder").GetComponent<ObjectBuilder>();

            var spriteRenderSize = transform.parent.GetComponent<SpriteRenderer>().bounds.size;
            var parentLocalSize = transform.parent.localScale;

            GetComponent<BoxCollider>().size = new Vector3(
                spriteRenderSize.x / parentLocalSize.x,
                spriteRenderSize.y / parentLocalSize.y,
                0.1f
            );
        }

        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            var tc = TouchController.GetTID();
            switch (TouchController.Mode)
            {
                case TouchMode.DeleteObject:
                    DeleteSensorParent();
                    isRayBlock = true;
                    break;
                case TouchMode.MoveObject:
                    tc._AlarmMe(touch.fingerId, this);
                    isRayBlock = true;
                    break;
                default:
                    isRayBlock = false;
                    break;
            }
        }

        public override void OnTouchMoved(Touch touch, out bool isRayBlock)
        {
            if (TouchController.Mode == TouchMode.DeleteObject)
            {
                isRayBlock = true;
                DeleteSensorParent();
            }
            isRayBlock = false;
        }

        private void DeleteSensorParent()
        {
            Destroy(transform.parent.gameObject);
        }

        public override void CallBack(Touch touch)
        {
            var newPosition = cam.ScreenToWorldPoint(touch.position);
            newPosition.z = 0;
            if(objectBuilder.isSnap && touch.phase == TouchPhase.Ended)
            {
                var objSize = transform.parent.GetComponent<SpriteRenderer>().bounds.size;
                objSize.y *= -1;
                newPosition-=objSize/2;

                newPosition.x = Mathf.Round(newPosition.x);
                newPosition.y = Mathf.Round(newPosition.y);

                newPosition+=objSize/2;
            }
            transform.parent.position = newPosition;
        }
    }
}
