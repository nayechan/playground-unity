using System;
using System.Collections;
using System.Collections.Generic;
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

    [Serializable]
    public class BlockConnections : ISerializationCallbackReceiver
    {
        public List<BlockConnection> blockConnections;

        public BlockConnections(List<BlockConnection> blockConnections)
        {
            this.blockConnections = blockConnections;
        }

        public void OnBeforeSerialize()
        {
            foreach (var connectionData in blockConnections)
            {
                connectionData.source.UpdateInstanceIDDataIfPortIsOnBlock();
                connectionData.destination.UpdateInstanceIDDataIfPortIsOnBlock();
            }
        }

        public void OnAfterDeserialize()
        {
        
        }
    }
    
}

