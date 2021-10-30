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
        var spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<CircleCollider2D>().radius = spriteRenderer.sprite.bounds.size.x/2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
