using GameEditor.EventEditor.Block;
using GameEditor.EventEditor.Controller;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class DeleteBlockButton : AbstractSensor
    {
        public override void OnTouchBegan(Touch touch, out bool isRayBlock) {
            isRayBlock = true;
            AbstractBlock block = GetComponentInParent<AbstractBlock>();
            BlockController.GetEBC().DestroyBlock(block);
        }
    }
}