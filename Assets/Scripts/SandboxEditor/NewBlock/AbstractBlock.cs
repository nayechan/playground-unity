using System;
using System.Collections.Generic;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public abstract class AbstractBlock : MonoBehaviour
    {
        public List<object> ports;
        public GameObject blockPrefab;

        private void Awake()
        {
            InitializePortData();
        }

        private void InitializePortData()
        {
            foreach (var port in GetComponentsInChildren<NewBlockPort>())
                port.portData.abstractBlock = this;
        }

        private void FixedUpdate()
        {
            BlockAction();
        }

        protected abstract void BlockAction();

        private void OnDestroy()
        {
            DestroyConnection();
        }

        private void DestroyConnection()
        {
        }

        protected abstract void OnGameStart();

        public abstract void MessageCallBack(string message);
    }

}
