using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGround : AbstractSensor
{
    private TouchControll _tc;
    protected override void Start(){
        _tc = GameObject.FindObjectOfType<TouchControll>();
    }

    public override void OnTouchBegan(Touch touch, out bool isRayBlock)
    {
        if(_tc.GetTouchMode() == "AddTile"){
            _tc.AddTile(touch);
            isRayBlock = true;
            return;
        }
        if(_tc.GetTouchMode() == "DelTile"){
            _tc.DelTile(touch);
            isRayBlock = true;
            return;
        }
        isRayBlock = false;
    }
    public override void OnTouchMoved(Touch touch, out bool isRayBlock)
    {
        if(_tc.GetTouchMode() == "AddTile"){
            _tc.AddTile(touch);
            isRayBlock = true;
            return;
        }
        if(_tc.GetTouchMode() == "DelTile"){
            _tc.DelTile(touch);
            isRayBlock = true;
            return;
        }
        isRayBlock = false;
    }
}
