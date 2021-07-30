using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlockController : MonoBehaviour
{
    private Dictionary<BlockProperty, List<SignalLine>> _blockToLines;
    // private GameObject _selectedBlock;
    private BlockPort _selectedPort;
    private string _mode;
    public GameObject signalLines;
    public GameObject blocks;
    public GameObject signalLineFab;
    public GameObject guideText;

    void Start(){
        _mode = "Pending";
    }

    void Update(){        
        if(Input.touchCount == 1){
            Touch t1 = Input.GetTouch(0);
            if(_mode == "EditBlock"){
                EditControll(t1);
            }
            if(_mode == "AddBlock"){
                AddBlock(t1);
            }
            if(_mode == "DelBlock"){
                DelBlock(t1);
            }
        }
    }

    private void DelBlock(Touch t1)
    {
        throw new NotImplementedException();
    }

    private void AddBlock(Touch t1)
    {
        throw new NotImplementedException();
    }

    void EditControll(Touch t1)
    {
        RaycastHit hit;
        if(!shotRay(t1, out hit)) return ;
        BlockPort port = hit.collider.GetComponent<BlockPort>();
        // 터치된 오브젝트가 Port 일경우.
        if(port != null && t1.phase != TouchPhase.Began){
            Debug.Log("hit");
            if(port.PortType == "Input" && _selectedPort !=null){
                // 블럭간의 선을 잇는다. 선 객체를 만든다.
                GameObject LineObj = Instantiate(signalLineFab, Vector3.zero, Quaternion.identity, signalLines.transform);
                LineObj.transform.parent = signalLines.transform;
                SignalLine signalLine = LineObj.AddComponent<SignalLine>();
                LineObj.GetComponent<LineRenderer>().SetPosition(0, _selectedPort.transform.position);               signalLine.SetLine(_selectedPort.block, _selectedPort.PortNum, port.block, port.PortNum);
                LineObj.GetComponent<LineRenderer>().SetPosition(1, port.transform.position);
                Debug.Log("New SignalLine constructed");
                // 라인을 블럭으로 인덱싱 한다. (블럭 파괴시 선도 없애기 위함)
                // BlockProperty leftBlock = _selectedPort.GetComponentInParent<BlockProperty>();
                // BlockProperty rightBlock = port.GetComponentInParent<BlockProperty>();
                // if(!_blockToLines.ContainsKey(leftBlock)) 
                // _blockToLines.Add(, signalLine);
                // _blockToLines.Add(port.GetComponentInParent<BlockProperty>(), signalLine);
                // 다음 라인 연결을 위한 초기화.
                _selectedPort = null;
                guideText.SetActive(false);
            }
            if(port.PortType == "Output" && _selectedPort == null){
                _selectedPort = port;
                Debug.Log("Selected Port set. please ");
                guideText.SetActive(true);
            }
        }
    }

    bool shotRay(Touch t1, out RaycastHit hit){
        Vector3 origin = Camera.main.ScreenToWorldPoint(new Vector3(t1.position.x, t1.position.y, 0f));
        bool isHit = Physics.Raycast(origin, Camera.main.transform.forward, out hit, 30f);
        if(isHit) Debug.Log("Hit! name is " + hit.transform.name);
        return isHit;
    }

    public void SetMode(string mode_){
        _mode = mode_;
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
