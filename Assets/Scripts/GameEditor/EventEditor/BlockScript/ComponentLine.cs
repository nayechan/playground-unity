using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentLine : SignalLine
{
    public override void SetLine(TouchSensor_BlockPort giver, TouchSensor_BlockPort reciver){
        base.SetLine(giver, reciver);
        reciver.body.SetAddComponentMethod(giver.body.GetAddComponentMethod());
    }

    void OnDestroy(){
        _reciver.SetAddComponentMethod((GameObject obj)=>{});
    }

    public override void PlayStart()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        if(lr != null) lr.enabled = false;
        _reciver.RunAddComponentMethod();
    }
}