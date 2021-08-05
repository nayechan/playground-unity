using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchController_obj : MonoBehaviour
{
    private Vector2 prevTouch;    
    private Vector3 prevPosition, worldCursor;
    private bool isOnCool = false, isSnap = false, isBlocked = false;
    public float sensitivity = 4.5f;
    private string _mode = "CameraMove";
    private Touch touch;
    private GameObject draggingObject = null;
    [SerializeField] private Camera cam;
    [SerializeField] private ObjectBuilder objectBuilder; 
    [SerializeField] private UnityEvent m_CamMoved;
    [SerializeField] private GridGuider gridGuide;

    void Start(){
        m_CamMoved = new UnityEvent();
        m_CamMoved.AddListener(gridGuide.WhenCamMoved);
    }
    void Update()
    {
        // Handle screen touches.
        if (Input.touchCount >= 1)
        {
            touch = Input.GetTouch(0);
            worldCursor = TouchToWorld();
            if(_mode == "CameraMove")
                ViewportDrag();
            if(_mode == "AddObject" && !isOnCool){
                AddObject();
            }
            if(_mode == "DelObject" && !isOnCool){
                DelObject();
                StartCoroutine("StartCooling", 0.1f);
            }
        }
    }

    void ViewportDrag()
    {
        switch(touch.phase)
        {
        case TouchPhase.Began:
        
            RaycastHit hit;

            if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                isBlocked = true;
                prevPosition = cam.transform.position;
                return;
            }
            else if(Physics.Raycast(worldCursor+new Vector3(0,0,-10), Vector3.forward, out hit, 100.0f))
            {
                Debug.Log(worldCursor);
                if(hit.transform.tag == "Object")
                {
                    isBlocked = true;
                    prevPosition = cam.transform.position;

                    hit.transform.GetComponent<ObjectInstanceController>().OnTouchDown();
                    draggingObject = hit.transform.gameObject;
                    return;
                }
            }
            else
            {
                prevTouch = touch.position;
                prevPosition = cam.transform.position;   
            }   

            break;

        case TouchPhase.Moved:

            if(!isBlocked)
            {
                Vector2 pos = touch.position - prevTouch;
                pos.x = pos.x / Screen.width * sensitivity;
                pos.y = pos.y / Screen.height * sensitivity;           
                cam.transform.position = prevPosition + new Vector3(-pos.x, -pos.y, 0.0f);
                m_CamMoved.Invoke();
            }
            else if(draggingObject != null)
            {
                draggingObject.GetComponent<ObjectInstanceController>().OnTouch();
            }

            break;

        case TouchPhase.Stationary:

            break;

        case TouchPhase.Ended:
        case TouchPhase.Canceled:

            if(!isBlocked)
                prevPosition = cam.transform.position;
            else if(draggingObject != null)
            {
                draggingObject.GetComponent<ObjectInstanceController>().OnTouchUp();
                draggingObject = null;
            }
            isBlocked = false;

            break;

        }
    }

    void AddObject(){
        switch(touch.phase)
        {
        case TouchPhase.Began:

            if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;
            bool res = objectBuilder.GenerateObject(RoundCursor(worldCursor));

            break;

        case TouchPhase.Moved:
        case TouchPhase.Stationary:
        case TouchPhase.Ended:
        case TouchPhase.Canceled:

            break;

        }
    }

    void DelObject(){
        switch(touch.phase)
        {
        case TouchPhase.Began:

            break;

        case TouchPhase.Moved:
        case TouchPhase.Stationary:

            if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }
            bool res = objectBuilder.RemoveObject(RoundCursor(worldCursor));
            if(res) Debug.Log("Tile removed around" + worldCursor.ToString());
            else Debug.Log("Failed to remove tile");

            break;

        case TouchPhase.Ended:
        case TouchPhase.Canceled:

            break;

        }        
    }

    public void SetTouchMode(string touchMode){
        Debug.Log("mode seted to : " + touchMode);
        _mode = touchMode;
    }
    
    Vector3 TouchToWorld(){
        return cam.ViewportToWorldPoint(new Vector3(
            (float)touch.position.x/Screen.width, 
            (float)touch.position.y/Screen.height, 
            -cam.transform.position.z)
        );    
    }

    Vector3 RoundCursor(Vector3 cursor){
        if(isSnap)
            return new Vector3(
                Mathf.Floor(cursor.x)+0.5f, 
                Mathf.Floor(cursor.y)+0.5f, 
                cursor.z
            );
        else
            return cursor;
    }

    IEnumerator StartCooling(float seconds){
        isOnCool = true;
        yield return new WaitForSeconds(seconds);
        isOnCool = false;
    }

    public void SetSnap(bool isSnap){this.isSnap = isSnap;}
    public void ToggleSnap(){isSnap = !isSnap;}

    public bool GetIsSnap(){return isSnap;}
}
