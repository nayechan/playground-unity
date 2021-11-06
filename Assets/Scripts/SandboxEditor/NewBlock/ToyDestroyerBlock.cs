using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class ToyDestroyerBlock : AbstractBlock
    {
        public BlockPort destroySignal;
        public BlockPort toyToDestroy;

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (destroySignal.value == null || (bool) destroySignal.value == false) return;
            Destroy((GameObject)toyToDestroy.value);
        }

        public override BlockData SaveBlockData()
        {
            var data = new ToyDestroyerBlockData();
            data.SetGameObjectIDAndPosition(this);
            return data;
        }
    }
}