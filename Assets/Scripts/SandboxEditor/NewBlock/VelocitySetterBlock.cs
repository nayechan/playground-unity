using System;
using SandboxEditor.Data;
using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class VelocitySetterBlock : AbstractBlock
    {
        public TextMesh currentVelocityPanel;
        public NewBlockPort setToyVelocityPort;
        public NewBlockPort signalPort;
        public float currentVelocity = 0f;

        public override void OnEveryFixedUpdate()
        {
        }

        public override BlockData SaveBlockData()
        {
            var data = new VelocitySetterBlockData(this);
            data.SetgameObjectIDAndPosition(this);
            return data;
        }

        public override void LoadBlockData(BlockData blockData)
        {
            base.LoadBlockData(blockData);
            currentVelocity = ((VelocitySetterBlockData) blockData).currentVelocity;
        }


        public override void WhenGameStart()
        {
        }

        public override void MessageCallBack(string message)
        {
            currentVelocity += float.Parse(message);
        }

        private void Update()
        {
            refreshVelocityPanel();
        }

        private void refreshVelocityPanel()
        {
            currentVelocityPanel.text = currentVelocity.ToString();
        }
        
        protected void InitializeBlockDataValue()
        {
            setToyVelocityPort.portData.Value = null;
            signalPort.portData.Value = false;
        }
    }
}