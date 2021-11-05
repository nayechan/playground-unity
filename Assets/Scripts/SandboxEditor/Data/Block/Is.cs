using System.Collections.Generic;
using SandboxEditor.InputControl.InEditor.Sensor;

namespace SandboxEditor.Data.Block
{
    public static class Is
    {
        private static readonly HashSet<(PortType, PortType)> correctPortTypePairs =
            new HashSet<(PortType, PortType)>
            {
                (PortType.BoolSender, PortType.BoolReceiver),
                (PortType.ScalarSender, PortType.ScalarReceiver),
                (PortType.ToyReceiver, PortType.ToySender)
            };
        
        private static readonly HashSet<(PortType, PortType)> SignalForwardToDestPairs =
            new HashSet<(PortType, PortType)>
            {
                (PortType.ToyReceiver, PortType.ToySender)
            };

        private static readonly HashSet<PortType> ConnectionStartTypes =
            new HashSet<PortType>()
            {
                PortType.BoolSender,
                PortType.ScalarSender,
                PortType.ToyReceiver
            };

        public static bool CorrectPortPair(PortType sourceType, PortType destType)
        {
            return correctPortTypePairs.Contains((sourceType, destType));
        }

        public static bool ReversedDirection(PortType sourceType, PortType destType)
        {
            return SignalForwardToDestPairs.Contains((sourceType, destType));
        }

        public static bool ConnectionStartType(PortType portType)
        {
            return ConnectionStartTypes.Contains(portType);
        }
    }
}