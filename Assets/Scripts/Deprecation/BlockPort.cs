using GameEditor.EventEditor.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace Deprecation
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
            BlockController.GetEBC().PortTouched(this);
        }
    }
}
