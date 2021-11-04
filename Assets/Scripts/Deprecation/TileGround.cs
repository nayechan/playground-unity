using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class TileGround : AbstractSensor
    {
        private TouchControl _tc;
        protected override void Start(){
            _tc = GameObject.FindObjectOfType<TouchControl>();
        }

        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            if(_tc.GetTouchMode() == "AddTile"){
                _tc.AddTile(touch);
                isRayBlock = true;
                return;
            }
            if(_tc.GetTouchMode() == "DelTile"){
                _tc.DelTile(touch);
                isRayBlock = true;
                return;
            }
            isRayBlock = false;
        }
        public override void OnTouchMoved(Touch touch, out bool isRayBlock)
        {
            if(_tc.GetTouchMode() == "AddTile"){
                _tc.AddTile(touch);
                isRayBlock = true;
                return;
            }
            if(_tc.GetTouchMode() == "DelTile"){
                _tc.DelTile(touch);
                isRayBlock = true;
                return;
            }
            isRayBlock = false;
        }
    }
}
