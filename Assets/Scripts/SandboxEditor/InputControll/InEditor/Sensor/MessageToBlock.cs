using GameEditor.EventEditor.Block;
using UnityEngine;

namespace GameEditor.EventEditor.UI.Sensor
{
    public class MessageToBlock : AbstractSensor
    {
        private AbstractBlock block;
        public string message;
        protected override void Start()
        {
            block = GetComponentInParent<AbstractBlock>();
        }
        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            isRayBlock = true;
            block.GetMessage(message);
        }
    }
}