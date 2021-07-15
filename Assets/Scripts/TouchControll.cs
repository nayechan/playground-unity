using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class TouchControll : MonoBehaviour
{
    private Vector2 _prevTouch;    
    private Vector3 _prevPosition;
    private Vector3 _worldCursor;
    private float _width;
    private float _height;
    private Enums.TouchMode _mode;
    private Touch _touch;
    private float Delay = 0.1f;
    public float sensitivity = 4.5f;
    public Camera cam;
    public TileBuilder tileBuilder; 

    void Awake()
    {
        _width = (float)Screen.width / 2.0f;
        _height = (float)Screen.height / 2.0f;
    }

    void Update()
    {
        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _worldCursor = TouchToWorld();
            Debug.Log("touch detected");
            if(_mode == Enums.TouchMode.CameraDrag)
                DragControll();
            if(_mode == Enums.TouchMode.TileAdd)
                AddTile();
            if(_mode == Enums.TouchMode.TileDel)
                DelTile();
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
            pos.x = pos.x / _width * sensitivity;
            pos.y = pos.y / _height * sensitivity;           
                            
            cam.transform.position = _prevPosition + new Vector3(-pos.x, -pos.y, 0.0f);
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

    public void SetTouchMode(Enums.TouchMode targetMode){
        Debug.Log("mode seted to : " + targetMode.ToString());
        _mode = targetMode;
    }

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);
    
        // GUI.Label(new Rect(50, 50, _width, _height * 0.25f),
        //     "x = " + cam.transform.position.x.ToString("f2") +
        //     ", y = " + cam.transform.position.y.ToString("f2"));

        GUI.Label(new Rect(50, 50, _width, _height * 0.25f),
            "x = " + _worldCursor.x +
            ", y = " + _worldCursor.y);
    }
    Vector3 TouchToWorld(){
        return cam.ViewportToWorldPoint(new Vector3((float)_touch.position.x/Screen.width, (float)_touch.position.y/Screen.height, -cam.transform.position.z));    
    }

    Vector3 RoundCursor(Vector3 cursor){
        return new Vector3(Mathf.Floor(cursor.x)+0.5f, Mathf.Floor(cursor.y)+0.5f, cursor.z);
    }
}
