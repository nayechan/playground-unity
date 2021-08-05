using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockProperty : MonoBehaviour
{
    // protected GameObject _attachedObject;
    public GameObject _attachedObject;
    protected float[] _inputs, _outputs;
    private int _lastFingerId;
    private List<SignalLine> _connectedLines;
    
    protected virtual void Start(){
        _connectedLines = new List<SignalLine>();
    }

    public virtual void Update(){
        BlockAction();
    }

    virtual protected void BlockAction(){}

    void OnDestroy(){
        foreach(var line in _connectedLines){
            if(line != null){
                Destroy(line.gameObject);
            }
        }
    }

    public void AddLine(SignalLine line){
        _connectedLines.Add(line);
    }

    public float getOutput(int portNum){
        return _outputs[portNum];
    }

    public void setInput(float val, int portNum){
        _inputs[portNum] = val;
    }

}
