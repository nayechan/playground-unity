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
        
        protected override void BlockAction()
        {
        }

        public override BlockData MakeBlockData()
        {
            var data = new VelocitySetterBlockData(this);
            data.SetIDAndPosition(this);
            return data;
        }

        public override void SetBlock(BlockData blockData)
        {
            base.SetBlock(blockData);
            currentVelocity = ((VelocitySetterBlockData) blockData).currentVelocity;
        }


        protected override void OnGameStart()
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
        
        protected override void InitializeBlockValue()
        {
            setToyVelocityPort.portData.Value = null;
            signalPort.portData.Value = false;
        }
    }
}