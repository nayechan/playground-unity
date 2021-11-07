using System;
using System.Collections;
using System.Collections.Generic;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.Data.Block
{
    [Serializable]
    public class PortConnectionData
    {
        public BlockPort sender;
        public BlockPort receiver;
        
        public PortData senderData;
        public PortData receiverData;
        public GameObject spriteLine;

        public PortConnectionData(BlockPort sender, BlockPort receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
            senderData = this.sender.portData;
            receiverData = this.receiver.portData;
        }

        public void SendSignal()
        {
            receiver.register?.ReceiveData(sender.register);
        }
        
    }

    [Serializable]
    public class BlockConnections : ISerializationCallbackReceiver
    {
        public List<PortConnectionData> blockConnections;

        public BlockConnections(List<PortConnectionData> blockConnections)
        {
            this.blockConnections = blockConnections;
        }

        public void OnBeforeSerialize()
        {
            foreach (var connectionData in blockConnections)
            {
                connectionData.senderData.UpdateInstanceIDDataIfPortIsOnBlock();
                connectionData.receiverData.UpdateInstanceIDDataIfPortIsOnBlock();
            }
        }

        public void OnAfterDeserialize()
        {
        
        }
    }
    
}

