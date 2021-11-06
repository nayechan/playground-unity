using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor;
using SandboxEditor.InputControl.InEditor.Sensor;
using SandboxEditor.InputControl.InPlay;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class TouchInputBlock : AbstractBlock
    {
        public BlockPort touchXAxisOutput;
        public BlockPort touchYAxisOutput;

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            var inputViewPort = PlayerTouchController.TouchToViewport();
            (touchXAxisOutput.value, touchYAxisOutput.value) = (inputViewPort.x, inputViewPort.y);
            Debug.Log($"value is {(float)touchXAxisOutput.value}, {(float)touchYAxisOutput.value}");
        }

        public override BlockData SaveBlockData()
        {
            var data = new TouchInputBlockData();
            data.SetGameObjectIDAndPosition(this);
            return data;
        }
        
    }
}