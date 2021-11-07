using System;
using GameEditor.EventEditor.Controller;
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
        private bool isConnectionSet = false;
        private SpriteRenderer senderSpriteRenderer;
        private SpriteRenderer receiverSpriteRenderer;
        private bool senderPortIsOnToy = false;
        private bool receiverPortIsOnToy = false;

        private void Update()
        {
            if (SandboxUpdateController.IsGameStarted) return;
            if (isConnectionSet && AreBothPortInvisible())
                ReRender();
            else
                lineRenderer.enabled = false;
        }

        public void SetConnection(PortConnectionData portConnectionData)
        {
            this.portConnectionData = portConnectionData;
            isConnectionSet = true;
            senderSpriteRenderer = portConnectionData.senderData.blockPort.GetComponent<SpriteRenderer>();
            receiverSpriteRenderer = portConnectionData.receiverData.blockPort.GetComponent<SpriteRenderer>();
            if (senderSpriteRenderer == null)
                senderPortIsOnToy = true;
            if (receiverSpriteRenderer == null)
                receiverPortIsOnToy = true;
        }

        private void ReRender()
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, portConnectionData.senderData.blockPort.transform.position);
            lineRenderer.SetPosition(1, portConnectionData.receiverData.blockPort.transform.position);
        }

        private bool AreBothPortInvisible()
        {
            return (senderPortIsOnToy || senderSpriteRenderer.enabled) &&
                   (senderPortIsOnToy || receiverSpriteRenderer.enabled);
        }

    }
}
