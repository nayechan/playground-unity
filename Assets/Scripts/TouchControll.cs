using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

public class TouchControll : MonoBehaviour
{
    private Vector2 _prevTouch;    
    private Vector3 _prevPosition;
    private Vector3 _worldCursor;
    private string _mode = "CameraMove";
    private Touch _touch;
    private float _deltaAfterAction = 0.0f;
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
            _touch = Input.GetTouch(0);
            _worldCursor = TouchToWorld();
            // Debug.Log("touch detected");
            if(_mode == "CameraMove")
                DragControll();
            if(_mode == "AddTile" && !IsAroundButton() && !IsOnCooling() ){
                AddTile();
                StartCooling();
            }
            if(_mode == "DelTile" && !IsAroundButton() && !IsOnCooling()){
                DelTile();
                StartCooling();
            }
        }
    }

    void DragControll(){

        if(_touch.phase == TouchPhase.Began){
            _prevPosition = _worldCursor;
        }
        // Move the cube if the screen has the finger moving.
        if (_touch.phase == TouchPhase.Moved)
        {
            cam.transform.Translate(_prevPosition - _worldCursor);
            Debug.Log(_prevPosition - _worldCursor);
            m_CamMoved.Invoke();
        }

    }

    void AddTile(){
        // Debug.Log("AddTile");        
        bool res = tileBuilder.GenerateTile(RoundCursor(_worldCursor));
        if(res) Debug.Log("Tile generated around" + _worldCursor.ToString());
        else Debug.Log("Failed to Generate tile");
    }

    void DelTile(){
        // Debug.Log("DelTile");
        bool res = tileBuilder.RemoveTile(RoundCursor(_worldCursor));
        if(res) Debug.Log("Tile removed around" + _worldCursor.ToString());
        else Debug.Log("Failed to remove tile");
    }

    public void SetTouchMode(string touchMode){
        Debug.Log("mode seted to : " + touchMode);
        _mode = touchMode;
    }

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = (int)(Screen.height / 20.0f);
        GUI.Label(new Rect(50, 50, Screen.width * 0.4f, Screen.height * 0.25f),
            "x = " + _worldCursor.x +
            ", y = " + _worldCursor.y);
    }
    Vector3 TouchToWorld(){
        return cam.ViewportToWorldPoint(new Vector3((float)_touch.position.x/Screen.width, (float)_touch.position.y/Screen.height, -cam.transform.position.z));    
    }

    Vector3 RoundCursor(Vector3 cursor){
        return new Vector3(Mathf.Floor(cursor.x)+0.5f, Mathf.Floor(cursor.y)+0.5f, cursor.z);
    }

    bool IsAroundButton(){
        if( (float)_touch.position.x / Screen.width > 0.7 && (float)_touch.position.y / Screen.height > 0.6 )
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
