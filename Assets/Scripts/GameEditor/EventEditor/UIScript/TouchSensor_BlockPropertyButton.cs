using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventEditorComponentSettings;

public class TouchSensor_BlockPropertyButton : TouchSensor
{
    private GameObject _prop;
    private BlockProperty _block;

    //추가된 항목 : PropertySettings가 null일 경우 properyWindow에 따라 componentSettings를 엽니다.
    [SerializeField] ComponentType propertyWindow;
    [SerializeField] EventEditorComponentSettings componentSettings;
    protected override void Start()
    {
        _block = GetComponentInParent<BlockProperty>();

        Transform trans = transform.Find("../../PropertySettings");
        if(trans == null) return;
        _prop = trans.gameObject;
    }
    public override void OnTouchBegan(Touch touch, out bool isRayBlock)
    {
        isRayBlock = true;
        // 기존 동작
        if(_prop != null)
        {
            _prop.SetActive(!_prop.activeSelf);
        }
        // PropertySettings가 null일 경우 componentSettings 열기
        else if(componentSettings != null && propertyWindow != ComponentType.None)
        {
            componentSettings.ActivateWindow(propertyWindow, _block);
        }
        
    }

}
