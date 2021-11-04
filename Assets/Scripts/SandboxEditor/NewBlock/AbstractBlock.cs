using System;
using System.Collections.Generic;
using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public abstract class AbstractBlock : MonoBehaviour
    {
        public List<object> ports;

        private void Awake()
        {
            InitializePortData();
        }

        private void InitializePortData()
        {
            foreach (var port in GetComponentsInChildren<NewBlockPort>())
                port.portData.abstractBlock = this;
        }


        protected abstract void BlockAction();

        private void OnDestroy()
        {
            DestroyConnection();
        }

        private void DestroyConnection()
        {
        }

        public abstract BlockData MakeBlockData();

        public virtual void SetBlock(BlockData blockData)
        {
            transform.position = blockData.position;
        }

        protected abstract void OnGameStart();
        
        public abstract void MessageCallBack(string message);
        
        private void FixedUpdate()
        {
            BlockAction();
        }
    }

}
