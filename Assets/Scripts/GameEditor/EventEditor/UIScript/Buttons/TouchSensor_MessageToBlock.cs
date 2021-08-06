using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor_MessageToBlock : TouchSensor
{
    private BlockProperty block;
    public string message;
    protected override void Start()
    {
        block = GetComponentInParent<BlockProperty>();
    }
    public override void OnTouchBegan(Touch touch, out bool isRayBlock)
    {
        isRayBlock = true;
        block.GetMessage(message);
    }
}