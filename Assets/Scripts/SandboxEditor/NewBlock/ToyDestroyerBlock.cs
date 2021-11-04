using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;

namespace SandboxEditor.NewBlock
{
    public class ToyDestroyerBlock : AbstractBlock
    {
        public NewBlockPort destroySignal;
        public NewBlockPort toyToDestroy;
        protected override void BlockAction()
        {
        }

        public override BlockData MakeBlockData()
        {
            var data = new ToyDestroyerBlockData();
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
            destroySignal.portData.Value = false;
            toyToDestroy.portData.Value = null;
        }
    }
}