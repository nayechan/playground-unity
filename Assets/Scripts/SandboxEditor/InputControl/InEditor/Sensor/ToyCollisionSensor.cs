﻿using System;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class ToyCollisionSensor : MonoBehaviour
    {
        public BlockPort port;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (port == null) return;
            BlockPort.WhenToyHit(other);
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            if (port == null) return;
            BlockPort.WhenToyHit(other);
        }
    }
}