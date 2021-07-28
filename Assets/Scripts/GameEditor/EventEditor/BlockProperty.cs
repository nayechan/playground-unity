using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockProperty : MonoBehaviour
{
    // protected GameObject _attachedObject;
    public GameObject _attachedObject;
    protected float[] _inputs, _outputs;
    public BlockProperty(int inputNum, int outputNum, GameObject obj = null){
        _inputs = new float[inputNum];
        _outputs = new float[outputNum];
        _attachedObject = obj;
    }

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
