using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPort : TouchSensor
{
    public string portType;
    public int portNum;
    public BlockProperty body;
    private EventBlockController _ebc;

    protected override void OnTouchBegan(Touch touch)
    {
        base.OnTouchBegan(touch);
        _ebc.portTouched(this);
    }

    protected override void Start() {
        _ebc = FindObjectOfType<EventBlockController>();
        body = GetComponentInParent<BlockProperty>();
        _blockRay = true;
    }

    public string PortType{
        get { return portType;}
    }
    public int PortNum{
        get { return portNum;}
    }
    public BlockProperty Body{
        get { return body;}
    }
}
