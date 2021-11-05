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
            if (CollisionInEveryFrame.HitToyAndOther.ContainsKey(toyToSense.gameObject))
            {
                Debug.Log($"Collision Detected {toyToSense.gameObject}");
                collisionDetected.Value = true;
                anotherToy.Value = CollisionInEveryFrame.HitToyAndOther[toyToSense.gameObject];
                Debug.Log($"another toy is {anotherToy.Value}");
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
            data.SetgameObjectIDAndPosition(this);
            return data;
        }
    }
}