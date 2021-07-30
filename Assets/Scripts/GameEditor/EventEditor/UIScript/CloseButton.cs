using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : TouchSensor
{
    private int _lastFingerId;

    public override void OnTouchDown(Touch touch)
    {
        _lastFingerId = touch.fingerId;
    }

    public override void OnTouchUp(Touch touch)
    {
        if(_lastFingerId == touch.fingerId){
            // 삭제관련 코드 추가
        }
    }
}
