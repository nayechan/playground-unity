using SandboxEditor.Data.Block;
using SandboxEditor.Data.Block.Register;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.Block
{
    public class AccelerationBlock : AbstractBlock
    {
        public BlockPort toyToAccelerate;
        public BlockPort xAxisInput;
        public BlockPort yAxisInput;

        private void Start()
        {
            InitializePortRegister();
        }

        protected override void InitializePortRegister()
        {
            toyToAccelerate.register = new ToyRegister();
            xAxisInput.register = new VectorRegister();
            yAxisInput.register = new VectorRegister();
        }

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (toyToAccelerate.RegisterValue == null) return;
            var targetToy = (GameObject) toyToAccelerate.RegisterValue;
            var rigidbody2D = targetToy.GetComponent<Rigidbody2D>();
            if (rigidbody2D == null) return;
            xAxisInput.RegisterValue ??= 0f;
            yAxisInput.RegisterValue ??= 0f;
            rigidbody2D.AddForce(new Vector2((float)xAxisInput.RegisterValue*10f, (float)yAxisInput.RegisterValue*10f));
        }

        public override BlockData SaveBlockData()
        {
            return new AccelerationBlockData(this);
        }
    }
}