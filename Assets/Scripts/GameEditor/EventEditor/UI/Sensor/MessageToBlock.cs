using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageToBlock : AbstractSensor
{
    private AbstractBlock block;
    public string message;
    protected override void Start()
    {
        block = GetComponentInParent<AbstractBlock>();
    }
    public override void OnTouchBegan(Touch touch, out bool isRayBlock)
    {
        isRayBlock = true;
        block.GetMessage(message);
    }
}