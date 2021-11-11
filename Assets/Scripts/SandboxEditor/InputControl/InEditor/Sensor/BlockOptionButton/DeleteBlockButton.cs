using UnityEngine;
using AbstractBlock = SandboxEditor.Block.AbstractBlock;

namespace SandboxEditor.InputControl.InEditor.Sensor.BlockOptionButton
{
    public class DeleteBlockButton : AbstractSensor
    {
        public GameObject BlockGameObject;
        public override void OnTouchBegan(Touch touch, out bool isRayBlock) {
            isRayBlock = true;
            Destroy(BlockGameObject);
        }
    }
}