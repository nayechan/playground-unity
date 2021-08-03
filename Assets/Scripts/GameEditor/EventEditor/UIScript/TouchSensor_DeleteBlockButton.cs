using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor_DeleteBlockButton : TouchSensor
{
    protected override void Start(){
        _blockRay = true; 
    }

    protected override void OnTouchBegan(Touch touch)
    {
        _lastFingerId = touch.fingerId;
    }

    protected override void OnTouchEnded(Touch touch)
    {
        if(_lastFingerId == touch.fingerId){
            BlockProperty block = GetComponentInParent<BlockProperty>();
            Destroy(block.gameObject);
        }
    }
}