using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlockController : MonoBehaviour
{
    /* 씬 안에 있는 Block들 및 SignalLine 들의 정보를 저장하고 관리하는 클래스입니다*/
    private TouchSensor_BlockPort _selectedOutputPort;
    public GameObject signalLines, blocks, signalLineFab, guideText;
    private static EventBlockController _ebc;
    private mode _mode;
    private enum mode{
        Editing,
        LineConnecting
    };

    void Start(){
        _mode = mode.Editing;
        _ebc = this;
    }

    static public EventBlockController GetEBC(){
        return _ebc;
    }

    public bool PortTouched(TouchSensor_BlockPort port){
        // 연결작업이 진행되었을 시 true 반환. 에디터 모드를 LineConnecting으로 변경.
        if(port.portType == "Output" && _mode == mode.Editing){
            _selectedOutputPort = port;
            _mode = mode.LineConnecting;
            guideText.SetActive(true);
            return true;
        }
        if(port.portType == "Input" && _mode == mode.LineConnecting){
            ConnectLine(port);
            _mode = mode.Editing;
            guideText.SetActive(false);
            return true;
        }
        return false;
    }
    
    public void ConnectLine(TouchSensor_BlockPort inputPort){
        // 선 객체를 만든다.
        GameObject LineObj = Instantiate(signalLineFab, Vector3.zero, Quaternion.identity, signalLines.transform);
        SignalLine signalLine = LineObj.AddComponent<SignalLine>();
        signalLine.SetLine(_selectedOutputPort.body, _selectedOutputPort.portNum, inputPort.body, inputPort.portNum);
        LineObj.GetComponent<LineRenderer>().SetPosition(0, _selectedOutputPort.transform.position); 
        LineObj.GetComponent<LineRenderer>().SetPosition(1, inputPort.transform.position);
        _selectedOutputPort.body.AddLine(signalLine);
        inputPort.body.AddLine(signalLine);
        Debug.Log("New SignalLine constructed");
    }

    public void GenerateBlockInstance(GameObject blockFab){
        if(blockFab.GetComponent<BlockProperty>() == null ) return ;
        Vector3 camPos = Camera.main.transform.position;
        Instantiate(blockFab, camPos - camPos.z * Vector3.forward, Quaternion.identity, blocks.transform); 
    }

    public void DestroyBlock(BlockProperty block){
        Destroy(block.gameObject);
    }

    public void CancelLineConnecting(){
        _mode = mode.Editing;
        guideText.SetActive(false);
        _selectedOutputPort = null;
    }
}
