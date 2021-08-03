using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TouchSensor : MonoBehaviour
{
    protected int _lastFingerId;
    protected bool _blockRay;

    protected abstract void Start();

    public virtual void Hit(Touch touch, out bool rayIsBlocked)
    {
        rayIsBlocked = _blockRay;
        switch(touch.phase){
            case TouchPhase.Began:
            OnTouchBegan(touch);
            break;
            case TouchPhase.Moved:
            OnTouchMoved(touch);
            break;
            case TouchPhase.Ended:
            OnTouchEnded(touch);
            break;
            case TouchPhase.Canceled:
            OnTouchCancled(touch);
            break;
            case TouchPhase.Stationary:
            OnTouchStationary(touch);
            break;
        }
    }
    protected virtual void OnTouchBegan(Touch touch){
        _lastFingerId = touch.fingerId;
    }
    protected virtual void OnTouchEnded(Touch touch){}
    protected virtual void OnTouchMoved(Touch touch){}
    protected virtual void OnTouchCancled(Touch touch){}
    protected virtual void OnTouchStationary(Touch touch){}

}
