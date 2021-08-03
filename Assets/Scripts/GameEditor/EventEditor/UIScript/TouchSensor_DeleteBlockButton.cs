using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor_DeleteBlockButton : TouchSensor
{
    public override void OnTouchBegan(Touch touch, out bool isRayBlock) {
        isRayBlock = true;
        BlockProperty block = GetComponentInParent<BlockProperty>();
        EventBlockController.GetEBC().DestroyBlock(block);
    }
}