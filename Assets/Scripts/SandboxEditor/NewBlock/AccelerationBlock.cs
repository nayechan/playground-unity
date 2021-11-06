using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class AccelerationBlock : AbstractBlock
    {
        public BlockPort toyToAccelerate;
        public BlockPort xAxisInput;
        public BlockPort yAxisInput;

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (toyToAccelerate.value == null) return;
            var targetToy = (GameObject) toyToAccelerate.value;
            var rigidbody2D = targetToy.GetComponent<Rigidbody2D>();
            if (rigidbody2D == null) return;
            // rigidbody2D.AddForce(new Vector2((float)xAxisInput.Value, (float)yAxisInput.Value));
            xAxisInput.value ??= 0f;
            yAxisInput.value ??= 0f;
            rigidbody2D.AddForce(new Vector2((float)xAxisInput.value*10f, (float)yAxisInput.value*10f));
        }

        public override BlockData SaveBlockData()
        {
            return new AccelerationBlockData(this);
        }
    }
}