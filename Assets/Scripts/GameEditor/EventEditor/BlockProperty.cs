using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockProperty : MonoBehaviour
{
    // protected GameObject _attachedObject;
    public GameObject _attachedObject;
    protected float[] _inputs, _outputs;
    
    public void Update(){
        BlockAction();
    }

    virtual protected void BlockAction(){}

    public float getOutput(int portNum){
        return _outputs[portNum];
    }

    public void setInput(float val, int portNum){
        _inputs[portNum] = val;
    }
}
