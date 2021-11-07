using SandboxEditor.Data.Block;
using SandboxEditor.Data.Block.Register;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class ToyDestroyerBlock : AbstractBlock
    {
        public BlockPort destroySignal;
        public BlockPort toyToDestroy;

        private void Start()
        {
            InitializePortRegister();
        }

        protected override void InitializePortRegister()
        {
            destroySignal.register = new BoolRegister();
            toyToDestroy.register = new ToyRegister();
        }

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (destroySignal.RegisterValue == null || (bool) destroySignal.RegisterValue == false) return;
            Destroy((GameObject)toyToDestroy.RegisterValue);
        }

        public override BlockData SaveBlockData()
        {
            return new ToyDestroyerBlockData(this);
        }
    }
}