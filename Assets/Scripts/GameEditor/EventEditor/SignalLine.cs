using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalLine : MonoBehaviour
{
    private BlockProperty _reciver;
    private int _reciverPort;
    private float _val;
    // Start is called before the first frame update

    SignalLine(BlockProperty reciver, int reciverPort){
        _reciverPort = reciverPort;
        _reciver = reciver;
    }

    void Update()
    {
        SendSignal();
    }

    public void ReciveSignal(float val){
        _val = val;        
    }

    public void SendSignal(){
        if(_reciver == null){
            Debug.Log("reciver is not atteched");
            return;
        }
        _reciver.ReciveSignal(_reciverPort, _val);
    }
}
