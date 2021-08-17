using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBlock : BlockProperty
{
    private static int _inputNum = 3, _outputNum = 0;

    protected override void Start(){
        base.Start();
        _inputs = new float[_inputNum];
        _outputs = new float[_outputNum];
    }

    protected override void BlockAction()
    {
        Rigidbody2D rb2d = _attachedObject.GetComponent<Rigidbody2D>();
        if(rb2d != null){
            rb2d.AddForce(new Vector2(_inputs[0],_inputs[1]));
        }
    }
}
