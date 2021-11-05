using System;
using SandboxEditor.Data;
using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace GameEditor.EventEditor.Line
{
    public sealed class ConnectionLine : MonoBehaviour
    {
        public BlockConnection blockConnection;
        public LineRenderer lineRenderer;
        private bool isSet = false;

        private void Awake()
        {
            var lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (isSet)
                ReLocation();
        }

        public void SetConnection(BlockConnection blockConnection)
        {
            this.blockConnection = blockConnection;
            isSet = true;
        }

        private void ReLocation()
        {
            lineRenderer.SetPosition(0, blockConnection.source.blockPort.transform.position);
            lineRenderer.SetPosition(1, blockConnection.destination.blockPort.transform.position);
        }

    }
}
