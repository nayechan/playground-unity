using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor_BlockMoveButton : TouchSensor
{
    // private Camera cam;
    private BlockProperty _block;
    // Update is called once per frame
    protected override void Start()
    {
        // cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        _block = GetComponentInParent<BlockProperty>();
    }

    public override void OnTouchBegan(Touch touch, out bool isRayBlock)
    {
        TouchInputDeliverer.GetTID().AlarmMe(touch.fingerId, this);
        isRayBlock = true;
    }

    // public override void CallBack(Touch touch){
    //     if(_block !=null){
    //         Vector3 newPos = Vector3.Scale(cam.ScreenToWorldPoint(touch.position), new Vector3(1,1,0));
    //         _block.OnBodyMove(newPos);
    //     }
    // }
    public override void CallBack(Touch touch){
        if(_block !=null){
            Vector3 newPos = Vector3.Scale(Camera.main.ScreenToWorldPoint(touch.position), new Vector3(1,1,0));
            _block.OnBodyMove(newPos);
        }
    }
}
