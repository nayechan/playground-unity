using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalLine : MonoBehaviour
{
    private BlockProperty _giver, _reciver;
    private int _giverPort, _reciverPort;
    private float _signal;
    private TouchSensor_BlockPort[] _ports;

    void Start(){
    }
    void Update()
    {
        ReciveSignal();
        SendSignal();
    }

    public void ReciveSignal(){
        _signal = _giver.getOutput(_giverPort);
    }

    public void SendSignal(){
        _reciver.setInput(_signal, _reciverPort);
    }

    public void SetLine(TouchSensor_BlockPort giver, TouchSensor_BlockPort reciver){
        _ports = new TouchSensor_BlockPort[2];
        this._giver = giver.body;
        this._reciver = reciver.body;
        this._giverPort = giver.portNum;
        this._reciverPort = reciver.portNum;
        Debug.Log("settig, "+ giver.ToString() + reciver.ToString());
        _ports[0] = giver;
        _ports[1] = reciver;
    }

    public void ReRendering(){
        LineRenderer rend = GetComponent<LineRenderer>();
        rend.SetPosition(0, _ports[0].transform.position);
        rend.SetPosition(1, _ports[1].transform.position);
    }
}
