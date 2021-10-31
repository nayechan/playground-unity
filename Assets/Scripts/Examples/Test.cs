using System;
using System.Collections;
using System.Collections.Generic;
using SandboxEditor.Data.Toy;
using Tools;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log($"other : {other.ToString()}, rigid body : {other.rigidbody}, this {this.ToString()}");
    //     other.rigidbody.velocity = new Vector2(0, 10f);
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10f);
    }
}
