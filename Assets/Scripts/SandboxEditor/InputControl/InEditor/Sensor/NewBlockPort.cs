using System;
using GameEditor.EventEditor.Controller;
using SandboxEditor.Data;
using SandboxEditor.Data.Block;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class NewBlockPort : AbstractSensor
    {
        public PortData portData;
        public int PortIndex => portData.portIndex;

        public PortType PortType
        {
            get => portData.portType;
            set => portData.portType = value;
        }

        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            isRayBlock = false;
            if (TouchController.Mode == TouchMode.CreateObject || TouchController.Mode == TouchMode.CreateBlock || TouchController.Mode == TouchMode.MoveObject) return;
            ConnectionController.WhenPortClicked(this);
            isRayBlock = true;
        }

        private void OnDestroy()
        {
            ConnectionController.DeleteConnections(this);
        }

    }
}
