using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class AccelerationBlock : AbstractBlock
    {
        public NewBlockPort toyToAccelerate;
        public NewBlockPort xAxisInput;
        public NewBlockPort yAxisInput;

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (toyToAccelerate.Value == null) return;
            Debug.Log($"im Acc block. x and y input is {(float)xAxisInput.Value}, {(float)yAxisInput.Value}");
            var targetToy = (GameObject) toyToAccelerate.Value;
            var rigidbody2D = targetToy.GetComponent<Rigidbody2D>();
            if (rigidbody2D == null) return;
            // rigidbody2D.AddForce(new Vector2((float)xAxisInput.Value, (float)yAxisInput.Value));
            rigidbody2D.AddForce(new Vector2((float)xAxisInput.Value*100f, (float)yAxisInput.Value*100f));
        }

        public override BlockData SaveBlockData()
        {
            var data = new AccelerationBlockData();
            data.SetGameObjectIDAndPosition(this);
            return data;
        }
    }
}