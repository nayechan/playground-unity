using System;
using GameEditor.EventEditor.Controller;
using SandboxEditor.Data;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class NewBlockPort : AbstractSensor
    {
        public PortData portData;
        public PortType PortType => portData.portType;

        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            isRayBlock = true;
            if (TouchController.Mode == TouchMode.CreateObject || TouchController.Mode == TouchMode.CreateBlock) return;
            ConnectionController.WhenPortClicked(this);
        }

        private void OnDestroy()
        {
            ConnectionController.DeleteConnections(this);
        }
    }
}
