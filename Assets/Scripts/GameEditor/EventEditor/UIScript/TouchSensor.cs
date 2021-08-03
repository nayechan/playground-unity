using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor : MonoBehaviour
{
    protected int _lastFingerId;
    protected virtual void Start(){
        _lastFingerId = -1;
    }
    public virtual void OnTouchBegan(Touch touch, out bool isRayBlock){
        _lastFingerId = touch.fingerId;
        isRayBlock = true;
    }
    public virtual void OnTouchEnded(Touch touch, out bool isRayBlock){
        if(touch.fingerId == _lastFingerId){
            _lastFingerId = -1;
        }
        isRayBlock = false;
    }
    public virtual void OnTouchMoved(Touch touch, out bool isRayBlock){
        isRayBlock = false;
    }
    public virtual void OnTouchCanceled(Touch touch, out bool isRayBlock){
        if(touch.fingerId == _lastFingerId){
            _lastFingerId = -1;
        }
        isRayBlock = false;
    }
    public virtual void OnTouchStationary(Touch touch, out bool isRayBlock){
        isRayBlock = false;
    }
}
