using System.Collections.Generic;
using SandboxEditor.InputControl.InEditor.Sensor;

namespace SandboxEditor.Data.Block
{
    public static class Is
    {
        private static readonly HashSet<(PortType, PortType)> correctPortTypePairs =
            new HashSet<(PortType, PortType)>
            {
                (PortType.Bool, PortType.Bool),
                (PortType.Scalar, PortType.Scalar),
                (PortType.ToyReceiver, PortType.Toy)
            };
        
        private static readonly HashSet<(PortType, PortType)> SignalForwardToDestPairs =
            new HashSet<(PortType, PortType)>
            {
                (PortType.ToyReceiver, PortType.Toy)
            };
        
        
        public static bool CorrectPortPair(PortType sourceType, PortType destType)
        {
            return correctPortTypePairs.Contains((sourceType, destType));
        }

        public static bool SignalForwardToDestPort(PortType sourceType, PortType destType)
        {
            return SignalForwardToDestPairs.Contains((sourceType, destType));
        }
    }
}