using SandboxEditor.Data.Block;
using SandboxEditor.Data.Block.Register;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.Block
{
    public class VelocitySetterBlock : AbstractBlock
    {
        public TextMesh XVelocityPanel;
        public TextMesh YVelocityPanel;
        public BlockPort ToyToSetVelocity;
        public BlockPort signalPort;
        public float XVelocity = 0f;
        public float YVelocity = 0f;

        protected override void InitializePortRegister()
        {
            ToyToSetVelocity.register = new ToyRegister();
            signalPort.register = new BoolRegister();
        }
        
        private void Update()
        {
            refreshVelocityPanel();
        }
        
        private void refreshVelocityPanel()
        {
            XVelocityPanel.text = "X : " + XVelocity;
            YVelocityPanel.text = "Y : " + YVelocity;
        }

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (ToyToSetVelocity.RegisterValue == null) return;
            var toy = (GameObject) ToyToSetVelocity.RegisterValue;
            Debug.Log($"Object Connected : {toy}");
            if ((bool)signalPort.RegisterValue == false) return;
            var rigidbody2D = toy.GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = new Vector2(XVelocity, YVelocity);
            Debug.Log($"signal On, set velocity of : {toy}");
        }

        public override void MessageCallBack(string message)
        {
            if(message[0] == 'x')
                XVelocity += float.Parse(message.Substring(1));
            if(message[0] == 'y')
                YVelocity += float.Parse(message.Substring(1));
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