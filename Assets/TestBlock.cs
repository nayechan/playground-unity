using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlock : BlockProperty
{
    private static int _inputNum = 1, _outputNum = 0;
    [SerializeField] TextMesh textMesh;
    

    protected override void Start(){
        base.Start();
        _inputs = new float[_inputNum];
        _outputs = new float[_outputNum];
    }

    public override void Update(){
        base.Update();
    }

    override protected void BlockAction(){
        textMesh.text = _inputs[0].ToString();
    }

    public override void GetMessage(string message)
    {
    }
}
