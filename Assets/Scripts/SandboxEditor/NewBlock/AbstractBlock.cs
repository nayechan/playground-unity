using System;
using System.Collections.Generic;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Storage;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public abstract class AbstractBlock : MonoBehaviour
    {
        public int gameObjectInstanceID;

        private void Awake()
        {
            gameObjectInstanceID = gameObject.GetInstanceID();
        }

        // 게임 시작시 매 프레임마다 실행되는 코드.
        public virtual void OnEveryFixedUpdate() {}

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
