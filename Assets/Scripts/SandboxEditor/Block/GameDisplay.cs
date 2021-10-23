using UnityEngine;

namespace GameEditor.EventEditor.Block
{
    public class GameDisplay: AbstractBlock
    {
        /* 입력 1 : 상하, 입력 2 : 좌우, 입력 3 : 지정된 액션 */
        private Camera _cam;
        private GameObject _area;

        void Awake(){
            // GameObject obj = new GameObject("PlayerDisplay", typeof(Camera));
            // obj.transform.parent = this.gameObject.transform;
            // _cam = obj.GetComponent<Camera>();
            _cam = this.gameObject.AddComponent<Camera>();
            _cam.backgroundColor = Color.black;
            _cam.clearFlags = CameraClearFlags.SolidColor;
        }
        protected override void Start(){
            base.Start();
            // _inputs = new float[_inputNum];
            // _outputs = new float[_outputNum];
            _area = this.transform.Find("PropertySettings/Area").gameObject;
            if(!_area) Debug.Log("ERROR! _AREA is NULL");
            _cam.orthographic = true;
            _cam.depth = -3f; // 다른 씬들의 카메라가 Deactivated 되면 자동으로 카메라가 켜짐.
            Resize(3f);
        }

        public override void GetMessage(string message)
        {
            if(message == "1"){
                _cam.orthographicSize += 1f;
                Resize(_cam.orthographicSize);
            }
            if(message == "-1"){
                _cam.orthographicSize -= 1f;
                Resize(_cam.orthographicSize);
            }
        }

        private void Resize(float height){
            _cam.orthographicSize = height;
            _area.transform.localScale = new Vector3(2f*(_cam.aspect*height),2f*height,1f);
        }

        public override void PlayStart()
        {
            base.PlayStart();
            Destroy(Camera.main);
            _cam.transform.position = this.gameObject.transform.position + new Vector3(0, 0, -10f);
        }
    }
}
