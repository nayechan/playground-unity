using System;
using SandboxEditor.Block;
using SandboxEditor.Data.Toy;
using SandboxEditor.InputControl.InEditor.Sensor;

namespace SandboxEditor.Data
{
    [Serializable]
    public class PortData
    {
        public int portIndex;
        public PortType portType;
        public BlockPort blockPort;

        public int gameObjectInstanceID;
        public AbstractBlock abstractBlock;

        public PortData(int portIndex, PortType portType, BlockPort blockPort)
        {
            this.portIndex = portIndex;
            this.portType = portType;
            this.blockPort = blockPort;
        }

        public void UpdateInstanceIDDataIfPortIsOnBlock()
        {
            gameObjectInstanceID = abstractBlock != null ? abstractBlock.gameObject.GetInstanceID() : blockPort.GetComponentInParent<ToySaver>().gameObject.GetInstanceID();
        }
    }

    public enum PortType
    {
        VectorReceiver,
        VectorSender,
        BoolReceiver,
        BoolSender,
        ToySender,
        ToyReceiver
    }
}
