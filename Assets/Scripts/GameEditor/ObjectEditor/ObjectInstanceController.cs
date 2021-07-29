using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstanceController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] TouchController_obj touchController;
    private Vector3 defaultSize, prevPos;
    private bool isOnTouch = false;
    private int imgIndex = 0;
    private string objectName, objectType;
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

            GetComponent<BoxCollider>().size = new Vector3(
                sprites[imgIndex].texture.width/sprites[imgIndex].pixelsPerUnit,
                sprites[imgIndex].texture.height/sprites[imgIndex].pixelsPerUnit,
                0
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
    public void SetObjectName(string name){
        objectName = name;
        gameObject.name = objectName;
    }
    public void SetObjectType(string type){objectType = type;}
    public string GetObjectName(){return objectName;}

    public void OnTouchDown()
    {
        prevPos = transform.position;
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(
                Input.GetTouch(0).position
        );
        currentPos.z = prevPos.z;
        isOnTouch = true;
    }

    public void OnTouch()
    {
        if(Input.touchCount == 0)
        {
            OnTouchUp();
            return;
        }
        Debug.Log("touch");
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(
                Input.GetTouch(0).position
        );
        currentPos.z = prevPos.z;
        transform.Translate(currentPos - prevPos);
            prevPos = currentPos;
    }

    public void OnTouchUp()
    {
        if(isOnTouch)
        {
            if(touchController.GetIsSnap())
            {
                transform.position =
                new Vector3(
                    Mathf.Floor(transform.position.x)+0.5f, 
                    Mathf.Floor(transform.position.y)+0.5f, 
                    transform.position.z
                );
            }
            prevPos = transform.position;
        }
        isOnTouch = false;
    }
}
