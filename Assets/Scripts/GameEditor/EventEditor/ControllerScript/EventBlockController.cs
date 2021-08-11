using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlockController : MonoBehaviour
{
    /* 씬 안에 있는 Block들 및 SignalLine 들의 정보를 저장하고 관리하는 클래스입니다*/
    public GameObject signalLineFab, guideText;
    private static EventBlockController _ebc;
    private TouchSensor_BlockPort _selectedOutputPort;
    private GameObject _signalLines, _blocks;
    private mode _mode;
    private enum mode{
        Editing,
        SignalLineConnecting,
        ComponentLineConnecting
    };

    void Start(){
        _mode = mode.Editing;
        _ebc = this;
        _signalLines = new GameObject("SignalLines");
        _blocks = new GameObject("Blocks");
    }

    static public EventBlockController GetEBC(){
        return _ebc;
    }

    public bool PortTouched(TouchSensor_BlockPort port){
        // 연결작업이 진행되었을 시 true 반환. 에디터 모드를 LineConnecting으로 변경.
        if(port.portType == "Output" && _mode == mode.Editing){
            _selectedOutputPort = port;
            _mode = mode.SignalLineConnecting;
            guideText.SetActive(true);
            return true;
        }
        if(port.portType == "Input" && _mode == mode.SignalLineConnecting){
            ConnectLine(port,"Signal");
            _mode = mode.Editing;
            guideText.SetActive(false);
            return true;
        }
        if(port.portType == "PropertyOutput" && _mode == mode.Editing){
            _selectedOutputPort = port;
            _mode = mode.ComponentLineConnecting;
            guideText.SetActive(true);
        }
        if(port.portType == "PropertyInput" && _mode == mode.ComponentLineConnecting){
            ConnectLine(port,"Component");
            _mode = mode.Editing;
            guideText.SetActive(false);
            return true;
        }
        return false;
    }
    
    public void ConnectLine(TouchSensor_BlockPort inputPort, string type){
        // 선 객체를 만든다.
        GameObject LineObj = Instantiate(signalLineFab, Vector3.zero, Quaternion.identity, _signalLines.transform);
        SignalLine signalLine;
        if(type == "Signal") signalLine = LineObj.AddComponent<SignalLine>();
        else signalLine = LineObj.AddComponent<ComponentLine>();
        signalLine.SetLine(_selectedOutputPort, inputPort);
        signalLine.ReRendering();
        _selectedOutputPort.body.AddLine(signalLine);
        inputPort.body.AddLine(signalLine);
        Debug.Log("New SignalLine constructed");
    }

    public void GenerateBlockInstance(GameObject blockFab){
        Vector3 camPos = Camera.main.transform.position;
        Instantiate(blockFab, camPos - camPos.z * Vector3.forward, Quaternion.identity, _blocks.transform); 
    }

    public void DestroyBlock(BlockProperty block){
        Destroy(block.gameObject);
    }

    public void CancelLineConnecting(){
        _mode = mode.Editing;
        guideText.SetActive(false);
        _selectedOutputPort = null;
    }

    public void PlaySart(){
        // 시작 초기 환경관련. 추후 다른 컴포넌트서 설정 가능
        Physics2D.gravity = Vector2.zero;
        //
        foreach(Transform line in _signalLines.GetComponentInChildren<Transform>()){
            line.GetComponent<SignalLine>().PlayStart();
        }
    }
}
