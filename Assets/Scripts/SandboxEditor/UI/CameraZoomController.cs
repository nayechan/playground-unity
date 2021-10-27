using UnityEngine;

namespace GameEditor.UI
{
    public class CameraZoomController : MonoBehaviour
    {
        public float ZoomUnit = 1.0f;
        public Camera cam;

        public void ZoomOut(){
            if(cam.orthographicSize < 12)
                cam.orthographicSize += ZoomUnit;
        }
        public void ZoomIn(){
            if(cam.orthographicSize > 1)
                cam.orthographicSize -= ZoomUnit;
        }
    }
}
