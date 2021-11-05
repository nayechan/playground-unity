using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class ToyDestroyerBlock : AbstractBlock
    {
        public NewBlockPort destroySignal;
        public NewBlockPort toyToDestroy;

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            if (destroySignal.Value == null || (bool) destroySignal.Value == false) return;
            Destroy((GameObject)toyToDestroy.Value);
        }

        public override BlockData SaveBlockData()
        {
            var data = new ToyDestroyerBlockData();
            data.SetGameObjectIDAndPosition(this);
            return data;
        }
    }
}