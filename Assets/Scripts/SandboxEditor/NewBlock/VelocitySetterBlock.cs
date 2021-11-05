using System;
using SandboxEditor.Data;
using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class VelocitySetterBlock : AbstractBlock
    {
        public TextMesh XVelocityPanel;
        public TextMesh YVelocityPanel;
        public NewBlockPort setToyVelocityPort;
        public NewBlockPort signalPort;
        public float XVelocity = 0f;
        public float YVelocity = 0f;

        private void Update()
        {
            refreshVelocityPanel();
        }
        
        private void refreshVelocityPanel()
        {
            XVelocityPanel.text = "X : " + XVelocity.ToString();
            YVelocityPanel.text = "Y : " + YVelocity.ToString();
        }

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (setToyVelocityPort.Value == null) return;
            if (signalPort.Value == null || (bool)signalPort.Value == false) return;
            var toy = (GameObject) setToyVelocityPort.Value;
            var rigidbody2D = toy.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = new Vector2(XVelocity, YVelocity);
        }



        public override void MessageCallBack(string message)
        {
            if(message[0] == 'x')
                XVelocity += float.Parse(message[1..]);
            if(message[0] == 'y')
                YVelocity += float.Parse(message[1..]);
        }

        public override BlockData SaveBlockData()
        {
            var data = new VelocitySetterBlockData(this);
            data.SetGameObjectIDAndPosition(this);
            return data;
        }
        
        public override void LoadBlockData(BlockData blockData)
        {
            base.LoadBlockData(blockData);
            XVelocity = ((VelocitySetterBlockData) blockData).XVelocity;
            YVelocity = ((VelocitySetterBlockData) blockData).YVelocity;
        }
    }
}