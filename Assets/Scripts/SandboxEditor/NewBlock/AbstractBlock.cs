using System;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Storage;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public abstract class AbstractBlock : MonoBehaviour
    {
        protected abstract void InitializePortRegister();

        public virtual void WhenGameStart()
        {
            DisableBlockRenderer();
        }
        
        private void DisableBlockRenderer()
        {
            foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
                spriteRenderer.enabled = false;
            foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
                meshRenderer.enabled = false;
        }

        public virtual void OnEveryFixedUpdateWhenPlaying() {}
        
        
        // 설정창이 있는 블럭에서 값을 조절하기 위해 사용.
        public virtual void MessageCallBack(string message) { }

        protected virtual void OnDestroy()
        {
            BlockStorage.RemoveBlock(this);
        }

        public abstract BlockData SaveBlockData();

        public virtual void LoadBlockData(BlockData blockData)
        {
            transform.position = blockData.position;
        }
    }

}
