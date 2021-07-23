using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstanceController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private Vector3 defaultSize;
    int imgIndex = 0;
    string objectName, objectType;
    // Start is called before the first frame update
    void Start()
    {
        defaultSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(sprites.Length > 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[imgIndex];
            transform.localScale = new Vector3(
                sprites[imgIndex].pixelsPerUnit/sprites[imgIndex].texture.width * defaultSize.x,
                sprites[imgIndex].pixelsPerUnit/sprites[imgIndex].texture.height * defaultSize.y,
                defaultSize.z
            );
            ++imgIndex;
            if(imgIndex >= sprites.Length)
                imgIndex = 0;
        }
        
    }

    public void SetSprites(Sprite[] sprites){
        this.sprites = sprites;
        foreach(Sprite sprite in sprites)
        {
            Debug.Log(sprite.rect);
            Debug.Log(sprite.pivot);
        }
    }

    public Vector3 getDefaultSize(){return defaultSize;}
    public void SetObjectName(string name){objectName = name;}
    public void SetObjectType(string type){objectType = type;}
    public string GetObjectName(){return objectName;}
}
