using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class TouchInputBlock : AbstractBlock
    {
        public NewBlockPort xAxis;
        public NewBlockPort yAxis;

        public override void OnEveryFixedUpdate()
        {
        }

        public override BlockData SaveBlockData()
        {
            var data = new TouchInputBlockData();
            data.SetgameObjectIDAndPosition(this);
            return data;
        }

        public override void WhenGameStart()
        {
        }

        public override void MessageCallBack(string message)
        {
        }
        
        protected void InitializeBlockDataValue()
        {
            xAxis.portData.Value = 0f;
            yAxis.portData.Value = 0f;
        }
    }
}