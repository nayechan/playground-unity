using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockProperty : MonoBehaviour
{
    private List<float> _inputs, _outputs;
    private List<SignalLine> _inputLines, _outputLines;

    public BlockProperty(int inputNum, int outputNum){
        _inputs = new List<float>(inputNum);
        _outputs = new List<float>(outputNum);
        _inputLines = new List<SignalLine>(inputNum);
        _outputLines = new List<SignalLine>(outputNum);
    }

    public void Update(){
        SendSignal();
    }

    virtual protected void BlockAction(){}
    
    private void SendSignal(){
        for(int i=0; i<_outputs.Count; i++){
            if(_outputLines[i] == null)
                continue;
            _outputLines[i].ReciveSignal(_outputs[i]);
        }
    }

    public void ReciveSignal(int inputPort, float val){
        _inputs[inputPort] = val;
    }
}
