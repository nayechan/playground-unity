using GameEditor.EventEditor.Controller;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Block.Register;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.Block
{
    public class CollisionDetector : AbstractBlock
    {
        public BlockPort toyToSense;
        public BlockPort collisionDetected;
        public BlockPort anotherToy;

        protected override void InitializePortRegister()
        {
            toyToSense.register = new ToyRegister();
            collisionDetected.register = new BoolRegister();
            if (anotherToy != null)
                anotherToy.register = new ToyRegister();
        }

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (toyToSense.RegisterValue == null) return;
            var targetToyGameObject = (GameObject) toyToSense.RegisterValue;
            if (CollisionInEveryFrame.HitToyAndOther.ContainsKey(targetToyGameObject))
            {
                collisionDetected.RegisterValue = true;
                anotherToy.RegisterValue = CollisionInEveryFrame.HitToyAndOther[targetToyGameObject];
            }
            else
            {
                collisionDetected.RegisterValue = false;
                anotherToy.RegisterValue = null;
            }
        }
        
        public override BlockData SaveBlockData()
        {
            return new CollisionDetectorData(this);
        }
    }
}