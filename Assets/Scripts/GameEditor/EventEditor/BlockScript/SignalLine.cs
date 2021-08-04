using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalLine : MonoBehaviour
{
    private BlockProperty _giver, _reciver;
    private int _giverPort, _reciverPort;
    private float _signal;

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

    public void SetLine(BlockProperty _giver, int _giverPort, BlockProperty _reciver, int _reciverPort){
        this._giver = _giver;
        this._reciver = _reciver;
        this._giverPort = _giverPort;
        this._reciverPort = _reciverPort;
    }
}
