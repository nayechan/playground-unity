using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareBlock : BlockProperty
{
    public enum Comparator{
        isLesserThan,
        isEqualOrLesserThan,
        isGreaterThan,
        isEqualOrGreaterThan,
        isEqual,
        isNotEqual
    }
    private static int _inputNum = 2, _outputNum = 1;
    [SerializeField] Comparator currentComparator;
    private float value;

    protected override void Start(){
        base.Start();
        currentComparator = Comparator.isLesserThan;
        _inputs = new float[_inputNum];
        _outputs = new float[_outputNum];
    }

    public override void Update(){
        base.Update();
    }

    override protected void BlockAction(){
        switch(currentComparator)
        {
            case Comparator.isLesserThan:
            _outputs[0] = (_inputs[0] < _inputs[1]) ? 1 : 0;
            break;
            case Comparator.isEqualOrLesserThan:
            _outputs[0] = (_inputs[0] <= _inputs[1]) ? 1 : 0;
            break;
            case Comparator.isGreaterThan:
            _outputs[0] = (_inputs[0] > _inputs[1]) ? 1 : 0;
            break;
            case Comparator.isEqualOrGreaterThan:
            _outputs[0] = (_inputs[0] >= _inputs[1]) ? 1 : 0;
            break;
            case Comparator.isEqual:
            _outputs[0] = (_inputs[0] == _inputs[1]) ? 1 : 0;
            break;
            case Comparator.isNotEqual:
            _outputs[0] = (_inputs[0] != _inputs[1]) ? 1 : 0;
            break;
        }
        Debug.Log(_inputs[0]+" "+currentComparator+" "+_inputs[1]+" = "+_outputs[0]);
    }

    public override void GetMessage(string message)
    {
        switch(message)
        {
            case "isLesserThan":
            currentComparator = Comparator.isLesserThan;
            break;
            case "isEqualOrLesserThan":
            currentComparator = Comparator.isEqualOrLesserThan;
            break;
            case "isGreaterThan":
            currentComparator = Comparator.isGreaterThan;
            break;
            case "isEqualOrGreaterThan":
            currentComparator = Comparator.isEqualOrGreaterThan;
            break;
            case "isEqual":
            currentComparator = Comparator.isEqual;
            break;
            case "isNotEqual":
            currentComparator = Comparator.isNotEqual;
            break;
        }
    }
}
