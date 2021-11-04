using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class TouchInputBlock : AbstractBlock
    {
        public NewBlockPort xAxis;
        public NewBlockPort yAxis;
        protected override void BlockAction()
        {
        }

        public override BlockData MakeBlockData()
        {
            var data = new TouchInputBlockData();
            data.SetIDAndPosition(this);
            return data;
        }

        protected override void OnGameStart()
        {
        }

        public override void MessageCallBack(string message)
        {
        }
        
        protected override void InitializeBlockValue()
        {
            xAxis.portData.Value = 0f;
            yAxis.portData.Value = 0f;
        }
    }
}