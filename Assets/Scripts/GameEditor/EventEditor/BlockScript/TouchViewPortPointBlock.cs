using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchViewPortPointBlock : BlockProperty
{
    private static int _inputNum = 0, _outputNum = 2;
    private UserInputController _userInput;

    protected override void Start(){
        base.Start();
        _inputs = new float[_inputNum];
        _outputs = new float[_outputNum];
        _userInput = GameObject.FindObjectOfType<UserInputController>();
    }
    override protected void BlockAction(){
        _outputs[0] = _userInput.GetTouchInputViewPort().x;
        _outputs[1] = _userInput.GetTouchInputViewPort().y;
    }
}
