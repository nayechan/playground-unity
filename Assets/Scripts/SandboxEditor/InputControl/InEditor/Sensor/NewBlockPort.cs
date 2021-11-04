using GameEditor.EventEditor.Block;
using GameEditor.EventEditor.Controller;
using SandboxEditor.Data;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class NewBlockPort : AbstractSensor
    {
        public PortData portData;

        public override void OnTouchBegan(Touch touch, out bool isRayBlock) {
            base.OnTouchBegan(touch, out isRayBlock);
            // 조건 충족하면 알람에 넣고 선 연결 관련 콜백함수 대충 호출
            // 조건충족여부, 연결 등등은 커넥션 컨트롤러에서 관리.
        }
    }
}
