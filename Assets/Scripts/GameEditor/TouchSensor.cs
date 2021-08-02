using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TouchSensor
{
    public virtual void OnTouchDown(Touch touch){}
    public virtual void OnTouchUp(Touch touch){}
    public virtual void OnDrag(Touch touch){}
}
