using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPort : AbstractSensor
{
    public string portType;
    public int portNum;
    public AbstractBlock body;


    protected override void Start() {
        base.Start();
        body = GetComponentInParent<AbstractBlock>();
    }

    public override void OnTouchBegan(Touch touch, out bool isRayBlock) {
        base.OnTouchBegan(touch, out isRayBlock);
        EventBlockController.GetEBC().PortTouched(this);
    }
}
