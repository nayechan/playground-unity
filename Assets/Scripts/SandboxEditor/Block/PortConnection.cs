using GameEditor.EventEditor.Controller;
using SandboxEditor.Data.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.Block
{
    public sealed class PortConnection : MonoBehaviour
    {
        public PortConnectionData portConnectionData;
        public LineRenderer lineRenderer;
        private bool isConnectionSet = false;
        private BlockBody senderPortBlockBody;
        private BlockBody receiverPortBlockBody;
        private bool senderPortIsOnToy = false;
        private bool receiverPortIsOnToy = false;

        private void Update()
        {
            if (SandboxPhaseChanger.IsGameStarted) return;
            if (isConnectionSet && AreBothPortInvisible())
                ReRender();
            else
                lineRenderer.enabled = false;
        }

        public void SetConnection(PortConnectionData portConnectionData)
        {
            this.portConnectionData = portConnectionData;
            isConnectionSet = true;
            senderPortBlockBody = portConnectionData.senderData.blockPort.GetComponentInParent<BlockBody>();
            receiverPortBlockBody = portConnectionData.receiverData.blockPort.GetComponentInParent<BlockBody>();
            if (senderPortBlockBody == null)
                senderPortIsOnToy = true;
            if (receiverPortBlockBody == null)
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
            return (senderPortIsOnToy || senderPortBlockBody.gameObject.activeSelf) &&
                   (receiverPortIsOnToy || receiverPortBlockBody.gameObject.activeSelf);
        }

    }
}
