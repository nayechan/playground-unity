using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlock : BlockProperty
{
    /* 입력 1 : 상하, 입력 2 : 좌우, 입력 3 : 지정된 액션 */
    private static int _inputNum = 3, _outputNum = 0;
    public float speed = 1f;
    private int _action;

    protected override void Start(){
        base.Start();
        _inputs = new float[_inputNum];
        _outputs = new float[_outputNum];
    }

    override protected void BlockAction(){
        _attachedObject.transform.Translate(_inputs[1] * Time.deltaTime * speed, 0, 0, Space.World);
        _attachedObject.transform.Translate(0, _inputs[0] * Time.deltaTime * speed, 0, Space.World);
        CharacterAction();
    }

    virtual public void CharacterAction(){}


}
