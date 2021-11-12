using System.Collections;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Block.Register;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.Block
{
    public class ToyDestroyerBlock : AbstractBlock
    {
        public BlockPort destroySignal;
        public BlockPort toyToDestroy;

        private void Awake()
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
            StartCoroutine(DestroyAfterMilliSec(((GameObject)toyToDestroy.RegisterValue)));
        }

        private IEnumerator DestroyAfterMilliSec(Object gameObject)
        {
            yield return new WaitForFixedUpdate();
            Destroy(gameObject);
        }

        public override BlockData SaveBlockData()
        {
            return new ToyDestroyerBlockData(this);
        }
    }
}