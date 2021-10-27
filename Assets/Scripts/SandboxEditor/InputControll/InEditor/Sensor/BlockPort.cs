using GameEditor.EventEditor.Block;
using GameEditor.EventEditor.Controller;
using UnityEngine;

namespace GameEditor.EventEditor.UI.Sensor
{
    public class BlockPort : AbstractSensor
    {
        public string portType;
        public int portNum;
        public AbstractBlock body;


        protected override void Start() {
            base.Start();
            body = GetComponentInParent<AbstractBlock>();
        }

        public override void OnTouchBegan(Touch touch, out bool isRayBlock) {
            base.OnTouchBegan(touch, out isRayBlock);
            EventBlockController.GetEBC().PortTouched(this);
        }
    }
}
