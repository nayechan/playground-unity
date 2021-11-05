using System;
using UnityEngine;

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
            // 신호가 정방향으로 가는 경우 sourceValue를 destinationValue에 입력한다.
            if (Is.SignalForwardToDestPort(source.portType, destination.portType))
                destination.Value = source.Value;
            else
                source.Value = destination.Value;
        }
        
    }
    
}

