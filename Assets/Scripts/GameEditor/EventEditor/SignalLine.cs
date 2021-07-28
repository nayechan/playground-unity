using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalLine : MonoBehaviour
{
    public BlockProperty giver, reciver;
    public int giverPort, reciverPort;
    private float _signal;

    // SignalLine(BlockProperty _giver, int _giverPort, BlockProperty _reciver, int _reciverPort){
    //     reciverPort = _reciverPort;
    //     reciver = _reciver;
    //     giverPort = _giverPort;
    //     giver = _giver;
    // }

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
}
