using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor_BlockBody : TouchSensor
{
    public override void OnTouchStationary(Touch touch, out bool isRayBlock)
    {
        base.OnTouchStationary(touch, out isRayBlock);
        // Debug.Log("Block Body Stationary");
    }



}
