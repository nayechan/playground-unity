using System;
using SandboxEditor.NewBlock;

namespace SandboxEditor.Data
{
    [Serializable]
    public class PortData
    {
        public int portIndex;
        public AbstractBlock abstractBlock;
        public PortType portType;
        public object Value
        {
            get => abstractBlock.ports[portIndex];
            set => abstractBlock.ports[portIndex] = value;
        }

        public PortData(AbstractBlock abstractBlock, int portIndex, PortType portType)
        {
            this.abstractBlock = abstractBlock;
            this.portIndex = portIndex;
            this.portType = portType;
        }
        
    }

    public enum PortType
    {
        Scalar,
        Bool,
        Toy,
        ToyReceiver
    }
}
