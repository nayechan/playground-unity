using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : AbstractBlock
{
    public float value = 0f;
    private static int _inputNum = 0, _outputNum = 1;

    protected override void Start(){
        base.Start();
        _inputs = new float[_inputNum];
        _outputs = new float[_outputNum];
    }

    public override void Update(){
        base.Update();
    }

    override protected void BlockAction(){
        _outputs[0] = value;
    }

    public void AddValue(float add){
        value += add;
        transform.Find("Body/Output_0/Text").GetComponent<TextMesh>().text =
            value.ToString("0.00");
    }

    public override void GetMessage(string message)
    {
        AddValue(float.Parse(message));
    }
}
