using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

public class TouchControll : MonoBehaviour
{
    Vector3 _prevPosition;
    float _prevDistance;
    float _prevCamSize;
    string _mode = "CameraMove";
    float _deltaAfterAction = 0.0f;
    public float timeForCooling = 0.1f;
    public Camera cam;
    public TileBuilder tileBuilder; 
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
        if (Input.touchCount > 0)
        {
            Touch t1 = Input.GetTouch(0);
            // Debug.Log("touch detected");
            // if(_mode == "Cameramove")
            //     DragControll(t1);
            if(_mode == "Addtile" && !IsAroundButton(t1) && !IsOnCooling() ){
                AddTile(t1);
                StartCooling();
            }
            if(_mode == "DelTile" && !IsAroundButton(t1) && !IsOnCooling()){
                DelTile(t1);
                StartCooling();
            }
        }
    }

    // void DragControll(Touch t1){
    //     if(Input.touchCount == 1){
    //         Vector3 worldCursor = TouchToWorld(t1);
    //         if(t1.phase == TouchPhase.Began){
    //         }
    //         if (t1.phase == TouchPhase.Moved){            
    //             cam.transform.position -= TouchToDelta(t1);
    //             m_CamMoved.Invoke();
    //         }
    //     }else if(Input.touchCount == 2){
    //         Touch t2 = Input.GetTouch(1);
    //         float prevDistance = (t1.position - t1.deltaPosition + t2.position - t2.deltaPosition).magnitude;
    //         float curDistance = (t1.position - t2.position).magnitude;
    //         cam.orthographicSize = Mathf.Clamp((prevDistance - curDistance) * 0.01f, 1f, 10f);
    //     }
    // }
    // TouchSensor, TouchInputDeliverer, 카메라 Collider 이용한 메커니즘으로 분리됨.

    void AddTile(Touch t1){
        // Debug.Log("AddTile");        
        Vector3 worldCursor = TouchToWorld(t1);
        bool res = tileBuilder.GenerateTile(RoundCursor(worldCursor));
        if(res) Debug.Log("Tile generated around" + worldCursor.ToString());
        else Debug.Log("Failed to Generate tile");
    }

    void DelTile(Touch t1){
        // Debug.Log("DelTile");
        Vector3 worldCursor = TouchToWorld(t1);
        bool res = tileBuilder.RemoveTile(RoundCursor(worldCursor));
        if(res) Debug.Log("Tile removed around" + worldCursor.ToString());
        else Debug.Log("Failed to remove tile");
    }

    public void SetTouchMode(string touchMode){
        Debug.Log("mode seted to : " + touchMode);
        _mode = touchMode;
    }

    Vector3 TouchToWorld(Touch t1){
        return cam.ScreenToWorldPoint(new Vector3(t1.position.x, t1.position.y, -cam.transform.position.z));    
    }

    Vector3 TouchToWorld(Vector2 pos){
        return cam.ViewportToWorldPoint(new Vector3((float)pos.x/Screen.width, (float)pos.y/Screen.height, -cam.transform.position.z));    
    }

    Vector3 TouchToDelta(Touch t1){
        Vector2 prev = t1.position - t1.deltaPosition;
        return TouchToWorld(t1) - TouchToWorld(prev);
    }
    Vector3 RoundCursor(Vector3 cursor){
        return new Vector3(Mathf.Floor(cursor.x)+0.5f, Mathf.Floor(cursor.y)+0.5f, cursor.z);
    }

    bool IsAroundButton(Touch t1){
        if( (float)t1.position.x / Screen.width > 0.7 && (float)t1.position.y / Screen.height > 0.6 )
            return true;
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
}
