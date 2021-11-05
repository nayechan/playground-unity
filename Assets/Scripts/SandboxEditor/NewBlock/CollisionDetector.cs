using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using Unity.VisualScripting;

namespace SandboxEditor.NewBlock
{
    public class CollisionDetector : AbstractBlock
    {
        public NewBlockPort toyToSense;
        public NewBlockPort collisionDetected;
        public NewBlockPort anotherToy;


        public override void OnEveryFixedUpdate()
        {
        }

        public override BlockData SaveBlockData()
        {
            var data = new CollisionDetectorData();
            data.SetgameObjectIDAndPosition(this);
            return data;
        }


        public override void WhenGameStart()
        {
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            InitializePortValue();
            
        }
        

        private void InitializePortValue()
        {
            anotherToy.portData.Value = null;
            toyToSense.portData.Value = null;
            collisionDetected.portData.Value = false;
        }
    }
}