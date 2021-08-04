using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor_BlockMoveButton : TouchSensor
{
    private Camera cam;
    private GameObject _targetObject;
    // Update is called once per frame
    protected override void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        _targetObject = GetComponentInParent<BlockProperty>().gameObject;
    }

    public override void OnTouchBegan(Touch touch, out bool isRayBlock)
    {
        TouchInputDeliverer.GetTID().AlarmMe(touch.fingerId, this);
        isRayBlock = true;
    }

    public override void CallBack(Touch touch){
        if(_targetObject !=null){
            _targetObject.transform.position = 
                Vector3.Scale(cam.ScreenToWorldPoint(touch.position), new Vector3(1,1,0));
        }
    }
}
