using System;
using SandboxEditor.Data;
using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace GameEditor.EventEditor.Line
{
    public sealed class PortConnection : MonoBehaviour
    {
        public PortConnectionData portConnectionData;
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

        public void SetConnection(PortConnectionData portConnectionData)
        {
            this.portConnectionData = portConnectionData;
            isSet = true;
        }

        private void ReLocation()
        {
            lineRenderer.SetPosition(0, portConnectionData.senderData.blockPort.transform.position);
            lineRenderer.SetPosition(1, portConnectionData.receiverData.blockPort.transform.position);
        }

    }
}
