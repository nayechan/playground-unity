using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor
{
    public class GridLinePositionUpdater : MonoBehaviour
    {
        private int _innerHalfBoxLength, _outterHalfBoxLength;
        private Vector2[] direction = new Vector2[]{new Vector2(0,0),new Vector2(-1,0),new Vector2(1,0),new Vector2(0,-1),new Vector2(0,1)};
        public Camera cam;
        public GameObject gridLines, xLine, yLine, xLineAxis, yLineAxis;

        void Start(){
            Vector3 camPos = cam.transform.position;
            _innerHalfBoxLength = 20; _outterHalfBoxLength = 23;
            for(int i= -_innerHalfBoxLength; i<_innerHalfBoxLength; ++i){
                Vector3 xPos = new Vector3(0, i, 0);
                Vector3 yPos = new Vector3(i, 0, 0);
                GameObject x = Instantiate<GameObject>(xLine, xPos, Quaternion.identity, gridLines.transform);
                GameObject y = Instantiate<GameObject>(yLine, yPos, Quaternion.identity, gridLines.transform);
            }
            FindObjectOfType<CameraBackground>().m_CameraMoved.AddListener(WhenCamMoved);

        }

        int DirOnOther(Vector2 objPos, Vector2 otherPos, float halfLengthOfBox){
            /*
        OnBox : return 0
        OutLeft : return 1
        OutRight : return 2
        OutBottom : return 3
        OutTop : return 4
        */
            if(objPos.x < otherPos.x - halfLengthOfBox) return 1;
            if(objPos.x > otherPos.x + halfLengthOfBox) return 2;
            if(objPos.y < otherPos.y - halfLengthOfBox) return 3;
            if(objPos.y > otherPos.y + halfLengthOfBox) return 4;
            return 0;
        }

        public void WhenCamMoved(){
            TranslateOutedLine();
            TranslateAxis();
        }
    
        private void TranslateOutedLine(){
            int childNum = gridLines.transform.childCount;
            for(int i=0; i<childNum; ++i){
                GameObject child = gridLines.transform.GetChild(i).gameObject;
                int res = DirOnOther(child.transform.position, cam.transform.position, _outterHalfBoxLength);
                if(res != 0){
                    Vector3 correction = direction[res] * _innerHalfBoxLength * -2.0f;
                    child.transform.Translate(correction);
                }
            }
        } 

        private void TranslateAxis(){
            xLineAxis.transform.position = new Vector3(cam.transform.position.x, 0, 0);
            yLineAxis.transform.position = new Vector3(0, cam.transform.position.y, 0);
        }

        public void ToggleGrid(){
            gridLines.SetActive(!gridLines.activeSelf);
        }
    }
}
