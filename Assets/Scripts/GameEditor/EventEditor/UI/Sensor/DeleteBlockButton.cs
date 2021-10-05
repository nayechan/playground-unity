using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBlockButton : AbstractSensor
{
    public override void OnTouchBegan(Touch touch, out bool isRayBlock) {
        isRayBlock = true;
        AbstractBlock block = GetComponentInParent<AbstractBlock>();
        EventBlockController.GetEBC().DestroyBlock(block);
    }
}