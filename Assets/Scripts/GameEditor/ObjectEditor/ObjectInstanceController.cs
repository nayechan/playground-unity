using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  애니메이션? 을 구성하는 클래스..
 */

public class ObjectInstanceController : MonoBehaviour
{
    [SerializeField] TouchController_obj touchController;
    private ObjectPrimitiveData primitiveData;
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
        if(primitiveData.GetSprites().Length > 0)
        {
            Sprite sprite = primitiveData.GetSprites()[imgIndex];
            GetComponent<SpriteRenderer>().sprite = sprite;
            transform.localScale = new Vector3(
                sprite.pixelsPerUnit/sprite.texture.width * defaultSize.x,
                sprite.pixelsPerUnit/sprite.texture.height * defaultSize.y,
                defaultSize.z
            );

            // GetComponent<BoxCollider>().size = new Vector3(
            //     sprite.texture.width/sprite.pixelsPerUnit,
            //     sprite.texture.height/sprite.pixelsPerUnit,
            //     0
            // );


            ++imgIndex;
            if(imgIndex >= primitiveData.GetSprites().Length)
                imgIndex = 0;
        }        
    }


    public void SetIsRaycastable(bool isRaycastable)
    {
        GetComponent<BoxCollider>().enabled = isRaycastable;
    }

    public Vector3 getDefaultSize(){return defaultSize;}

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

    public void SetObjectPrimitiveData(ObjectPrimitiveData data)
    {
        primitiveData = data;
    }

    public ObjectPrimitiveData GetObjectPrimitiveData()
    {
        return primitiveData;
    }

    public void setTouchController(TouchController_obj touchController)
    {
        this.touchController = touchController;
    }
}
