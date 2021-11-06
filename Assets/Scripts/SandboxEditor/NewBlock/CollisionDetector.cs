using System;
using System.Linq;
using GameEditor.EventEditor.Controller;
using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class CollisionDetector : AbstractBlock
    {
        public BlockPort toyToSense;
        public BlockPort collisionDetected;
        public BlockPort anotherToy;

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (toyToSense.value == null) return;
            var targetToyGameObject = (GameObject) toyToSense.value;
            if (CollisionInEveryFrame.HitToyAndOther.ContainsKey(targetToyGameObject))
            {
                collisionDetected.value = true;
                anotherToy.value = CollisionInEveryFrame.HitToyAndOther[targetToyGameObject];
            }
            else
            {
                collisionDetected.value = false;
                anotherToy.value = null;
            }
        }
        
        public override BlockData SaveBlockData()
        {
            return new CollisionDetectorData(this);
        }
    }
}