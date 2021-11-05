using System;
using System.Linq;
using SandboxEditor.Data.Toy;
using SandboxEditor.InputControl.InEditor.Sensor;
using SandboxEditor.NewBlock;
using UnityEngine;

namespace SandboxEditor.Data
{
    [Serializable]
    public class PortData
    {
        public int portIndex;
        public PortType portType;
        public NewBlockPort blockPort;

        public int gameObjectInstanceID;
        public AbstractBlock abstractBlock;
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

        public PortData(int portIndex, PortType portType, NewBlockPort blockPort)
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
        ScalarReceiver,
        ScalarSender,
        BoolReceiver,
        BoolSender,
        ToySender,
        ToyReceiver
    }
}
