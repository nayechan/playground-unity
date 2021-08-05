using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor_BlockPropertyButton : TouchSensor
{
    private GameObject _prop;
    protected override void Start()
    {
        Transform trans = transform.Find("../../PropertySettings");
        if(trans == null) return;
        _prop = trans.gameObject;
    }
    public override void OnTouchBegan(Touch touch, out bool isRayBlock)
    {
        isRayBlock = true;
        if(_prop == null) return;
        _prop.SetActive(!_prop.activeSelf);
    }

}
