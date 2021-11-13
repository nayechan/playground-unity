using GameEditor.EventEditor.Controller;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Storage;
using UnityEngine;

namespace SandboxEditor.Block
{
    public abstract class AbstractBlock : MonoBehaviour, PhaseChangeCallBackReceiver
    {
        protected virtual void Awake()
        {
            InitializePortRegister();
        }

        protected abstract void InitializePortRegister();

        public virtual void WhenGameStart()
        {
            DisableBlockRenderer();
        }

        public virtual void WhenTestStart()
        {
            DisableBlockRenderer();
        }

        public virtual void WhenTestPause() { }

        public virtual void WhenTestResume() { }

        public virtual void WhenBackToEditor() { }
        
        public virtual void OnEveryFixedUpdateWhenPlaying() {}
        
        private void DisableBlockRenderer()
        {
            foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
                spriteRenderer.enabled = false;
            foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
                meshRenderer.enabled = false;
        }
        
        
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
