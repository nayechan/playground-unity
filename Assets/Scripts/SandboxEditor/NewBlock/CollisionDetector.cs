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
        public NewBlockPort toyToSense;
        public NewBlockPort collisionDetected;
        public NewBlockPort anotherToy;

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (toyToSense.Value == null) return;
            var targetToyGameObject = (GameObject) toyToSense.Value;
            if (CollisionInEveryFrame.HitToyAndOther.ContainsKey(targetToyGameObject))
            {
                collisionDetected.Value = true;
                anotherToy.Value = CollisionInEveryFrame.HitToyAndOther[targetToyGameObject];
            }
            else
            {
                collisionDetected.Value = false;
                anotherToy.Value = null;
            }
        }
        
        public override BlockData SaveBlockData()
        {
            var data = new CollisionDetectorData();
            data.SetGameObjectIDAndPosition(this);
            return data;
        }
    }
}