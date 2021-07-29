using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlockController : MonoBehaviour
{
    private HashSet<GameObject> _objs;
    private HashSet<GameObject> _blocks;
    // private HashSet<GameObject> _signalLines;
    private GameObject _selectedBlock;
    private BlockPort _selectedPort;
    private string _mode;
    public GameObject signalLines;
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
                GameObject LineObj = Instantiate(signalLineFab, Vector3.zero, Quaternion.identity, signalLines.transform);
                LineObj.transform.parent = signalLines.transform;
                SignalLine signalLine = LineObj.AddComponent<SignalLine>();
                LineObj.GetComponent<LineRenderer>().SetPosition(0, _selectedPort.transform.position);               signalLine.SetLine(_selectedPort.block, _selectedPort.PortNum, port.block, port.PortNum);
                LineObj.GetComponent<LineRenderer>().SetPosition(1, port.transform.position);
                Debug.Log("New SignalLine constructed");
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

    public void UpdateNewObjs(List<GameObject> allObjs){
        foreach(GameObject obj in allObjs){
            if(_objs.Contains(obj))
                continue;
            // MakeNewBlock(obj);
            _objs.Add(obj);
        }
    }

    // private void MakeNewBlock(GameObject obj){
    //     // obj의 타입을 얻는다.
    //     string type = "objType";
    //     //
    // }

    public void SelectBlock(GameObject block){
        _selectedBlock = block;
    }

    bool shotRay(Touch t1, out RaycastHit hit){
        Vector3 origin = Camera.main.ScreenToWorldPoint(new Vector3(t1.position.x, t1.position.y, 0f));
        bool isHit = Physics.Raycast(origin, Camera.main.transform.forward, out hit, 30f);
        Debug.Log(hit.point);
        return isHit;
    }

    public void SetMode(string mode_){
        _mode = mode_;
    }
}
