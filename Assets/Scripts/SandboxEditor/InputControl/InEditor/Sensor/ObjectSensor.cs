using System;
using SandboxEditor.Builder;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class ObjectSensor : AbstractSensor
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
            isRayBlock = true;
            var tc = TouchController.GetTID();
            switch (tc.mode)
            {
                case TouchController.TouchMode.DeleteObject:
                    DeleteSensorParentIfDeleteMode();
                    break;
                case TouchController.TouchMode.MoveObject:
                    tc.AlarmMe(touch.fingerId, this);
                    break;
                default:
                    break;
            }
        }

        public override void OnTouchMoved(Touch touch, out bool isRayBlock)
        {
            isRayBlock = true;
            DeleteSensorParentIfDeleteMode();
        }

        private void DeleteSensorParentIfDeleteMode()
        {
            if (TouchController.GetTID().mode == TouchController.TouchMode.DeleteObject)
                Destroy(transform.parent.gameObject);
        }

        public override void OnTouchStationary(Touch touch, out bool isRayBlock)
        {
            isRayBlock = true;
            if(TouchController.GetTID().mode == TouchController.TouchMode.DeleteObject)
            {
                Destroy(transform.parent.gameObject);
            }
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
