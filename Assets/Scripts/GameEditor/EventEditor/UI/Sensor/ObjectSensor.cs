using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameEditor.EventEditor.UI.Sensor
{
    public class ObjectSensor : AbstractSensor
    {
        Vector3 _touchBeginPosition;
        Camera cam;
        ObjectBuilder objectBuilder;
        protected override void Start()
        {
            cam = Camera.main;

            objectBuilder = 
            GameObject.Find("ObjectBuilder").GetComponent<ObjectBuilder>();

            Vector3 spriteRenderSize = 
            transform.parent.GetComponent<SpriteRenderer>().bounds.size;

            Vector3 parentLocalSize = transform.parent.localScale;

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
            _touchBeginPosition = touch.position;
            switch (tc.mode)
            {
                case TouchController.TouchMode.CamMove:
                    break;

                case TouchController.TouchMode.CreateObject:
                    break;
                
                case TouchController.TouchMode.DeleteObject:
                    break;

                case TouchController.TouchMode.MoveObject:
                    Debug.Log(touch.position);
                    tc.AlarmMe(touch.fingerId, this);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void CallBack(Touch touch)
        {
            Debug.Log(touch.position);
            var tc = TouchController.GetTID();
            Debug.Log(touch.phase);

            Vector3 worldPos, deltaVector;
            worldPos = touch.position;
            worldPos = Camera.main.ScreenToWorldPoint(worldPos);

            switch (tc.mode)
            {
            case TouchController.TouchMode.CamMove:
                break;

            case TouchController.TouchMode.CreateObject:
                break;

            case TouchController.TouchMode.DeleteObject:
                break;
                
            case TouchController.TouchMode.MoveObject:
                Vector3 v = cam.ScreenToWorldPoint(touch.position);
                v.z = 0;

                if(objectBuilder.isSnap && touch.phase == TouchPhase.Ended)
                {
                    Vector3 objSize = 
                    transform.parent.GetComponent<SpriteRenderer>().bounds.size;

                    v.x = Mathf.Floor(v.x);
                    v.y = Mathf.Floor(v.y);

                    Vector3 pivotAmount = objSize;
                    pivotAmount.x *= 0.5f;
                    pivotAmount.y *= 0.5f;
                    pivotAmount.z = 0;

                    v += pivotAmount;
                }
                transform.parent.position = v;
                break;

            }
        }
    }
}
