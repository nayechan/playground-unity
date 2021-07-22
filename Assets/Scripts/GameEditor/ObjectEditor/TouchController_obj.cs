using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchController_obj : MonoBehaviour
{
    private Vector2 _prevTouch;    
    private Vector3 _prevPosition;
    private Vector3 _worldCursor;
    private string _mode = "CameraMove";
    private Touch _touch;
    private float _deltaAfterAction = 0.0f;
    public float timeForCooling = 0.1f;
    public float sensitivity = 4.5f;
    public Camera cam;
    public ObjectBuilder objectBuilder; 
    public UnityEvent m_CamMoved;
    public GridGuider gridGuide;

    void Start(){
        m_CamMoved = new UnityEvent();
        m_CamMoved.AddListener(gridGuide.WhenCamMoved);
    }
    void Update()
    {
        // Handle screen touches.
        _deltaAfterAction += Time.deltaTime;
        if (Input.touchCount == 1)
        {
            _touch = Input.GetTouch(0);
            _worldCursor = TouchToWorld();
            // Debug.Log("touch detected");
            if(_mode == "CameraMove")
                DragControll();
            if(_mode == "AddObject" && !IsAroundButton() && !IsOnCooling() 
            && _touch.phase == TouchPhase.Began){
                AddTile();
                StartCooling();
            }
            if(_mode == "DelObject" && !IsAroundButton() && !IsOnCooling()){
                DelTile();
                StartCooling();
            }
        }
    }

    void DragControll(){

        if(_touch.phase == TouchPhase.Began){
            _prevTouch = _touch.position;
            _prevPosition = cam.transform.position;
        }
        // Move the cube if the screen has the finger moving.
        if (_touch.phase == TouchPhase.Moved)
        {
            Vector2 pos = _touch.position - _prevTouch;
            pos.x = pos.x / Screen.width * sensitivity;
            pos.y = pos.y / Screen.height * sensitivity;           
            cam.transform.position = _prevPosition + new Vector3(-pos.x, -pos.y, 0.0f);
            m_CamMoved.Invoke();
        }

        if (_touch.phase == TouchPhase.Ended){
            _prevPosition = cam.transform.position;
        }

        // if (Input.touchCount == 2)
        // {
        //     touch = Input.GetTouch(1);

        //     if (touch.phase == TouchPhase.Began)
        //     {
        //         // Halve the size of the cube.
        //         transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        //     }

        //     if (touch.phase == TouchPhase.Ended)
        //     {
        //         // Restore the regular size of the cube.
        //         cam.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        //     }
        // }
    }

    void AddTile(){
        // Debug.Log("AddTile");        
        bool res = objectBuilder.GenerateObject(RoundCursor(_worldCursor));
        if(res) Debug.Log("Tile generated around" + _worldCursor.ToString());
        else Debug.Log("Failed to Generate tile");
    }

    void DelTile(){
        // Debug.Log("DelTile");
        bool res = objectBuilder.RemoveObject(RoundCursor(_worldCursor));
        if(res) Debug.Log("Tile removed around" + _worldCursor.ToString());
        else Debug.Log("Failed to remove tile");
    }

    public void SetTouchMode(string touchMode){
        Debug.Log("mode seted to : " + touchMode);
        _mode = touchMode;
    }
    
    Vector3 TouchToWorld(){
        return cam.ViewportToWorldPoint(new Vector3((float)_touch.position.x/Screen.width, (float)_touch.position.y/Screen.height, -cam.transform.position.z));    
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

    bool IsAroundButton(){
        // if( (float)_touch.position.x / Screen.width > 0.7 && (float)_touch.position.y / Screen.height > 0.6 )
        //     return true;
        return false;
    }

    bool IsOnCooling(){
        if(_deltaAfterAction > timeForCooling){            
            return false;
        }
        return true;
    }

    void StartCooling(){
        _deltaAfterAction = 0f;
    }

    bool isSnap = false;
    public void setSnap(bool isSnap){this.isSnap = isSnap;}
    public void toggleSnap(){isSnap = !isSnap;}
}
