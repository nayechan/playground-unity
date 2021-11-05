using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor;
using SandboxEditor.InputControl.InEditor.Sensor;
using SandboxEditor.InputControl.InPlay;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class TouchInputBlock : AbstractBlock
    {
        public NewBlockPort touchXAxisOutput;
        public NewBlockPort touchYAxisOutput;

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            var inputViewPort = PlayerTouchController.TouchToViewport();
            (touchXAxisOutput.Value, touchYAxisOutput.Value) = (inputViewPort.x, inputViewPort.y);
            Debug.Log($"Value is {(float)touchXAxisOutput.Value}, {(float)touchYAxisOutput.Value}");
        }

        public override BlockData SaveBlockData()
        {
            var data = new TouchInputBlockData();
            data.SetGameObjectIDAndPosition(this);
            return data;
        }
        
    }
}