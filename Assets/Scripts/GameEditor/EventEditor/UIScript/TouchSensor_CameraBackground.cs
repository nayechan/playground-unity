using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchSensor_CameraBackground : TouchSensor
{
    private Camera cam;
    private Vector2 _touchBeginPosition;
    private Vector3 _camBeginPosition;
    public UnityEvent m_CameraMoved;

    protected override void Start()
    {
        cam = GetComponent<Camera>();
        m_CameraMoved = new UnityEvent();
    }

    public override void OnTouchBegan(Touch touch, out bool isRayBlock)
    {
        TouchInputDeliverer.GetTID().AlarmMe(touch.fingerId, this);
        _touchBeginPosition = touch.position;
        _camBeginPosition = cam.transform.position;
        isRayBlock = true;
    }

    public override void CallBack(Touch touch)
    {
        Vector3 deltaVector = 
            cam.ScreenToWorldPoint(touch.position) - cam.ScreenToWorldPoint(_touchBeginPosition);
        cam.transform.position = _camBeginPosition - deltaVector;
        m_CameraMoved.Invoke();
    }
}
