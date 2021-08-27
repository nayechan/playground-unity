using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchViewPortPoint : AbstractBlock
{
    private static int _inputNum = 0, _outputNum = 2;
    private UserInputController _userInput;
    private Camera cam;

    protected override void Start(){
        base.Start();
        _inputs = new float[_inputNum];
        _outputs = new float[_outputNum];
        _userInput = GameObject.FindObjectOfType<UserInputController>();
    }
    override protected void BlockAction(){
        Camera cam = GameObject.FindObjectOfType<GameDisplay>().GetComponent<Camera>();
        if(cam == null) return;
        _outputs[0] = _userInput.GetTouchInputViewPort(cam).x;
        _outputs[1] = _userInput.GetTouchInputViewPort(cam).y;
    }
}
