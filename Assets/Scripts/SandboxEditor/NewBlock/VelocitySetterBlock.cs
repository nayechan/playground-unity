using System;
using SandboxEditor.Data;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class VelocitySetterBlock : AbstractBlock
    {
        public TextMesh currentVelocityPanel;
        public NewBlockPort SetToyVelocityPort;
        public NewBlockPort signalPort;
        private float currentVelocity = 0f;
        
        protected override void BlockAction()
        {
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
    }
}