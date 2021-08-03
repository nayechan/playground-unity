using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickRightLeftBlock : BlockProperty
{
    /* 입력 1 : 상하, 입력 2 : 좌우, 입력 3 : 지정된 액션 */
    private static int _inputNum = 0, _outputNum = 1;
    private UserInputController _userInput;

    protected override void Start(){
        base.Start();
        _inputs = new float[_inputNum];
        _outputs = new float[_outputNum];
        _userInput = GameObject.Find("/Scripts").GetComponent<UserInputController>();
    }
    override protected void BlockAction(){
        _outputs[0] = _userInput.GetStickLInput().x;
    }

    virtual public void CharacterAction(){}
}
