using SandboxEditor.Data.Block;
using SandboxEditor.Data.Block.Register;
using SandboxEditor.InputControl.InEditor.Sensor;
using SandboxEditor.InputControl.InPlay;
using UnityEngine;

namespace SandboxEditor.Block
{
    public class TouchInputBlock : AbstractBlock
    {
        public BlockPort touchXAxisOutput;
        public BlockPort touchYAxisOutput;

        private void Awake()
        {
            InitializePortRegister();
        }

        protected override void InitializePortRegister()
        {
            touchXAxisOutput.register = new VectorRegister();
            touchYAxisOutput.register = new VectorRegister();
        }

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            var inputViewPort = PlayerTouchController.TouchToViewport();
            (touchXAxisOutput.RegisterValue, touchYAxisOutput.RegisterValue) = (inputViewPort.x, inputViewPort.y);
            Debug.Log($"value is {(float)touchXAxisOutput.RegisterValue}, {(float)touchYAxisOutput.RegisterValue}");
        }

        public override BlockData SaveBlockData()
        {
            return new TouchInputBlockData(this);
        }
        
    }
}