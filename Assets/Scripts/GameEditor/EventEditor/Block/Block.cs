using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : AbstractBlock
{
    private static int _inputNum = 3, _outputNum = 0;
    private bool _onPlay;
    protected MyListener _Listener;
    public float speed = 1f;
    private int _action;

    protected override void Start(){
        base.Start();
        _inputs = new float[_inputNum];
        _outputs = new float[_outputNum];
        _onPlay = false;
    }

    protected override void BlockAction()
    {
        if(_attachedObject == null) return;
        _attachedObject.transform.Translate(_inputs[1] * Time.deltaTime * speed, 0, 0, Space.World);
        _attachedObject.transform.Translate(0, _inputs[0] * Time.deltaTime * speed, 0, Space.World);
        Collider2D col = _attachedObject.GetComponent<Collider2D>();
        if(col){
            List<Collider2D> _ = new List<Collider2D>();
            int num = col.OverlapCollider(new ContactFilter2D().NoFilter(), _);
            if(num > 0 ) OnTriggerEnter2d(col);
        }
    }
    public override void PlayStart()
    {
        _onPlay = true;
    }

    void OnTriggerEnter2d(Collider2D col){
        if(_onPlay){
            StartCoroutine("DestroyBlock");
        }
    }

    IEnumerator DestroyBlock(){
        yield return new WaitForSeconds(0.1f);
        Destroy(_attachedObject.gameObject);
    }
}
