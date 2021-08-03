using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor_BlockBody : TouchSensor
{

    public override void OnTouchMoved(Touch touch, out bool isRayBlock){
        Camera cam = TouchInputDeliverer.cam;
        if(touch.fingerId != _lastFingerId) {
            isRayBlock = false;
            return;
        }
        GetComponentInParent<BlockProperty>().gameObject.transform.position =
            Vector3.Scale(cam.ScreenToWorldPoint(touch.position), new Vector3(1,1,0));
        isRayBlock = true;
    }


}
