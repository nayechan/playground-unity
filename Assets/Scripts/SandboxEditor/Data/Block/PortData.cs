using System;
using System.Linq;
using SandboxEditor.InputControl.InEditor.Sensor;
using SandboxEditor.NewBlock;

namespace SandboxEditor.Data
{
    [Serializable]
    public class PortData
    {
        public int portIndex;
        public AbstractBlock abstractBlock;
        public PortType portType;
        public NewBlockPort BlockPort => abstractBlock.ports[portIndex];
        private object value;
        public object Value
        {
            get => value;
            set
            {
                if(value != null)
                    this.value = value;   
            }
        }

        public PortData(AbstractBlock abstractBlock, int portIndex, PortType portType)
        {
            this.abstractBlock = abstractBlock;
            this.portIndex = portIndex;
            this.portType = portType;
        }

        public NewBlockPort GetPort()
        {
            return abstractBlock.ports[portIndex];
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
