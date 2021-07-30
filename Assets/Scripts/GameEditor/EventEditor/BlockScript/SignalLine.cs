using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalLine : MonoBehaviour
{
    public BlockProperty giver, reciver;
    public int giverPort, reciverPort;
    private float _signal;

    void Update()
    {
        ReciveSignal();
        SendSignal();
    }

    public void ReciveSignal(){
        _signal = giver.getOutput(giverPort);
    }

    public void SendSignal(){
        reciver.setInput(_signal, reciverPort);
    }

    public void SetLine(BlockProperty _giver, int _giverPort, BlockProperty _reciver, int _reciverPort){
        giver = _giver;
        reciver = _reciver;
        giverPort = _giverPort;
        reciverPort = _reciverPort;
    }
}
