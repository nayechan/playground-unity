using UnityEngine;

namespace GameEditor.EventEditor.UI.Sensor
{
    public class BlockBody : AbstractSensor
    {
        public override void OnTouchStationary(Touch touch, out bool isRayBlock)
        {
            base.OnTouchStationary(touch, out isRayBlock);
            // Debug.Log("Block Body Stationary");
        }



    }
}
