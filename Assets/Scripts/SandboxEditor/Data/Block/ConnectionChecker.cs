using System.Collections.Generic;
using SandboxEditor.InputControl.InEditor.Sensor;

namespace SandboxEditor.Data.Block
{
    public static class ConnectionChecker
    {
        // 추후 별도의 포트 클래스로 관리하는 걸 고려할 것.
        public static readonly HashSet<(PortType, PortType)> correctSenderReceiverPairs =
            new HashSet<(PortType, PortType)>
            {
                (PortType.BoolSender, PortType.BoolReceiver),
                (PortType.ScalarSender, PortType.ScalarReceiver),
                (PortType.ToySender, PortType.ToyReceiver),
            };
        
        public static readonly HashSet<(PortType, PortType)> correctCombinations =
            new HashSet<(PortType, PortType)>
            {
                (PortType.BoolSender, PortType.BoolReceiver),
                (PortType.ScalarSender, PortType.ScalarReceiver),
                (PortType.ToySender, PortType.ToyReceiver),
                (PortType.BoolReceiver, PortType.BoolSender),
                (PortType.ScalarReceiver, PortType.ScalarSender),
                (PortType.ToyReceiver, PortType.ToySender),
            };
        
        public static readonly HashSet<PortType> senderTypes =
            new HashSet<PortType>()
            {
                PortType.BoolSender,
                PortType.ScalarSender,
                PortType.ToySender,
            };

        public static readonly HashSet<PortType> receiverTypes =
            new HashSet<PortType>()
            {
                PortType.BoolReceiver,
                PortType.ScalarReceiver,
                PortType.ToyReceiver
            };
    }
}