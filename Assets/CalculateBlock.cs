using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateBlock : BlockProperty
{
    public enum Operator{
        Add,
        Subtract,
        Multiply,
        Divide
    }
    private static int _inputNum = 2, _outputNum = 1;
    [SerializeField] Operator currentOperator;
    private float value;

    protected override void Start(){
        base.Start();
        currentOperator = Operator.Add;
        _inputs = new float[_inputNum];
        _outputs = new float[_outputNum];
    }

    public override void Update(){
        base.Update();
    }

    override protected void BlockAction(){
        switch(currentOperator)
        {
            case Operator.Add:
            _outputs[0] = _inputs[0] + _inputs[1];
            break;
            case Operator.Subtract:
            _outputs[0] = _inputs[0] - _inputs[1];
            break;
            case Operator.Multiply:
            _outputs[0] = _inputs[0] * _inputs[1];
            break;
            case Operator.Divide:
            _outputs[0] = _inputs[0] / _inputs[1];
            break;
        }
    }

    public override void GetMessage(string message)
    {
        Debug.Log(message);
        switch(message)
        {
        case "Add":
            currentOperator = Operator.Add;
            break;
        case "Subtract":
            currentOperator = Operator.Subtract;
            break;
        case "Multiply":
            currentOperator = Operator.Multiply;
            break;
        case "Divide":
            currentOperator = Operator.Divide;
            break;
        }
    }
}
