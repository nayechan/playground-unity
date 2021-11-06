using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor.BlockOptionButton
{
    public class BlockMoveButton : AbstractSensor
    {
        public GameObject blockGameObject;

        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            TouchController.GetTID()._AlarmMe(touch.fingerId, this);
            isRayBlock = true;
        }
        public override void CallBack(Touch touch)
        {
            var newPos = Vector3.Scale(Camera.main.ScreenToWorldPoint(touch.position), new Vector3(1,1,0));
            blockGameObject.transform.position = newPos;
        }
    }
}
