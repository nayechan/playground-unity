using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstanceController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    int imgIndex = 0;
    string objectName, objectType;
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("pos").GetComponent<TextMesh>().text = "("+transform.position.x+","+transform.position.y+")";
    }

    // Update is called once per frame
    void Update()
    {
        if(sprites.Length > 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[imgIndex];
            ++imgIndex;
            if(imgIndex >= sprites.Length)
                imgIndex = 0;
        }
        
    }

    public void SetSprites(Sprite[] sprites){this.sprites = sprites;}
    public void SetObjectName(string name){objectName = name;}
    public void SetObjectType(string type){objectType = type;}
    public string GetObjectName(){return objectName;}
}
