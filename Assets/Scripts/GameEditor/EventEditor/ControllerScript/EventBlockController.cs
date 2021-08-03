using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlockController : MonoBehaviour
{
    /* 씬 안에 있는 Block들 및 SignalLine 들의 정보를 저장하고 관리하는 클래스입니다*/
    private Dictionary<BlockProperty, List<SignalLine>> _blockToLines;
    private BlockPort _selectedOutputPort;
    public GameObject signalLines, blocks, signalLineFab, guideText;
    private mode _mode;
    private enum mode{
        Editing,
        LineConnecting
    };

    void Start(){
        _mode = mode.Editing;
    }

    // void Update(){        
    //     if(Input.touchCount == 1){
    //         Touch t1 = Input.GetTouch(0);
    //     }
    // }
    public bool portTouched(BlockPort port){
        // 연결작업이 진행되었을 시 true 반환. 에디터 모드를 LineConnecting으로 변경.
        if(port.portType == "Output" && _selectedOutputPort == null){
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

    void EditControll(Touch t1)
    {
        RaycastHit hit;
        if(!shotRay(t1, out hit)) return ;
        BlockPort port = hit.collider.GetComponent<BlockPort>();
        if(port == null || t1.phase != TouchPhase.Began) return;
        // 터치된 오브젝트가 Port 일경우.
        if(port.PortType == "Input" && _selectedOutputPort !=null ){
            ConnectLine(port);
            return ;
        }
        if(port.PortType == "Output" && _selectedOutputPort == null){
            _selectedOutputPort = port;
            return ;
        }

    }
    public void ConnectLine(BlockPort inputPort){
        // 블럭간의 선을 잇는다. 선 객체를 만든다.
        GameObject LineObj = Instantiate(signalLineFab, Vector3.zero, Quaternion.identity, signalLines.transform);
        LineObj.transform.parent = signalLines.transform;
        SignalLine signalLine = LineObj.AddComponent<SignalLine>();
        LineObj.GetComponent<LineRenderer>().SetPosition(0, _selectedOutputPort.transform.position);               signalLine.SetLine(_selectedOutputPort.body, _selectedOutputPort.PortNum, inputPort.body, inputPort.PortNum);
        LineObj.GetComponent<LineRenderer>().SetPosition(1, inputPort.transform.position);
        Debug.Log("New SignalLine constructed");
        // 라인을 블럭으로 인덱싱 한다. (블럭 파괴시 선도 없애기 위함)
        // BlockProperty leftBlock = _selectedPort.GetComponentInParent<BlockProperty>();
        // BlockProperty rightBlock = port.GetComponentInParent<BlockProperty>();
        // if(!_blockToLines.ContainsKey(leftBlock)) 
        // _blockToLines.Add(, signalLine);
        // _blockToLines.Add(port.GetComponentInParent<BlockProperty>(), signalLine);
        // 다음 라인 연결을 위한 초기화.
        _selectedOutputPort = null;
        guideText.SetActive(false);
    }

    bool shotRay(Touch t1, out RaycastHit hit){
        Vector3 origin = Camera.main.ScreenToWorldPoint(new Vector3(t1.position.x, t1.position.y, 0f));
        bool isHit = Physics.Raycast(origin, Camera.main.transform.forward, out hit, 30f);
        return isHit;
    }

    public void GenerateBlockInstance(GameObject blockFab){
        if(blockFab.GetComponent<BlockProperty>() == null ) return ;
        Vector3 camPos = Camera.main.transform.position;
        Instantiate(blockFab, camPos - camPos.z * Vector3.forward, Quaternion.identity, blocks.transform); 
        return ;
    }

    public void DestroyBlock(GameObject block){
        BlockProperty prop = block.GetComponent<BlockProperty>();
        if(prop == null) return;

    }
}
