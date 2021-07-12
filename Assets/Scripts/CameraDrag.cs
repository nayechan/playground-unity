using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector2 prevTouch;    
    private Vector3 prevPosition;
    private float width;
    private float height;
    public float sensitivity = 1.0f;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

    }

    void OnGUI()
    {
        // Compute a fontSize based on the size of the screen width.
        GUI.skin.label.fontSize = (int)(Screen.width / 25.0f);
    
        GUI.Label(new Rect(50, 50, width, height * 0.25f),
            "x = " + transform.position.x.ToString("f2") +
            ", y = " + transform.position.y.ToString("f2"));
    }

    void Update()
    {
        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);            
            if(touch.phase == TouchPhase.Began){
                prevTouch = touch.position;
                prevPosition = transform.position;
            }
            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position - prevTouch;
                //pos.x = (pos.x - width) / width;
                //pos.y = (pos.y - height) / height;     
                pos.x = pos.x / width * sensitivity;
                pos.y = pos.y / height * sensitivity;           
                                
                transform.position = prevPosition + new Vector3(-pos.x, -pos.y, 0.0f);
            }

            if (touch.phase == TouchPhase.Ended){
                prevPosition = transform.position;
            }

            if (Input.touchCount == 2)
            {
                touch = Input.GetTouch(1);

                if (touch.phase == TouchPhase.Began)
                {
                    // Halve the size of the cube.
                    transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    // Restore the regular size of the cube.
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
            }
        }
    }
}
