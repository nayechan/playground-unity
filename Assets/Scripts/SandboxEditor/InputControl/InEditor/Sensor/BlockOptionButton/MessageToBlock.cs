using UnityEngine;
using AbstractBlock = SandboxEditor.NewBlock.AbstractBlock;

namespace SandboxEditor.InputControl.InEditor.Sensor.BlockOptionButton
{
    public class MessageToBlock : AbstractSensor
    {
        public AbstractBlock block;
        public string message;
        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            isRayBlock = true;
            block.MessageCallBack(message);
        }
    }
}