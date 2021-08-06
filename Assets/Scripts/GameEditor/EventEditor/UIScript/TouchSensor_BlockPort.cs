using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor_BlockPort : TouchSensor
{
    public string portType;
    public int portNum;
    public BlockProperty body;


    protected override void Start() {
        base.Start();
        body = GetComponentInParent<BlockProperty>();
    }

    public override void OnTouchBegan(Touch touch, out bool isRayBlock) {
        base.OnTouchBegan(touch, out isRayBlock);
        EventBlockController.GetEBC().PortTouched(this);
    }
}
