using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
