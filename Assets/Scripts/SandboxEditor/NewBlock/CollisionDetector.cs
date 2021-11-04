using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;

namespace SandboxEditor.NewBlock
{
    public class CollisionDetector : AbstractBlock
    {
        public NewBlockPort toyToSense;
        public NewBlockPort collisionDetected;
        public NewBlockPort anotherToy;
        

        protected override void BlockAction()
        {
        }

        public override BlockData MakeBlockData()
        {
            var data = new CollisionDetectorData();
            data.SetIDAndPosition(this);
            return data;
        }
        
        
        protected override void OnGameStart()
        {
        }

        public override void MessageCallBack(string message)
        {
        }
    }
}