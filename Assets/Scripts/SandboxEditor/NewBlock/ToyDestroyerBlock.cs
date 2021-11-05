using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;

namespace SandboxEditor.NewBlock
{
    public class ToyDestroyerBlock : AbstractBlock
    {
        public NewBlockPort destroySignal;
        public NewBlockPort toyToDestroy;

        public override void OnEveryFixedUpdate()
        {
        }

        public override BlockData SaveBlockData()
        {
            var data = new ToyDestroyerBlockData();
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
            destroySignal.portData.Value = false;
            toyToDestroy.portData.Value = null;
        }
    }
}