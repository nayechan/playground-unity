using System;
using SandboxEditor.NewBlock;
using UnityEngine;
using static SandboxEditor.Data.PortType;

namespace SandboxEditor.Data.Block
{
    [Serializable]
    public class BlockConnection
    {
        public PortData source;
        public PortData destination;
        public GameObject spriteLine;

        public BlockConnection(PortData source, PortData destination)
        {
            this.source = source;
            this.destination = destination;
        }

        public void SendSignal()
        {
            if (source.portType == ToyReceiver && destination.portType == PortType.Toy)
                source.Value = destination.Value;
            else
                destination.Value = source.Value;
        }
        
    }
    
}

