using System;
using SandboxEditor.NewBlock;

namespace SandboxEditor.Data.Block
{
    [Serializable]
    public class VelocitySetterBlockData : BlockData
    {
        public float currentVelocity;
        public int setToyVelocityPortID;
        public int signalPortID;

        public VelocitySetterBlockData(VelocitySetterBlock block)
        {
            SetIDAndPosition(block);
            currentVelocity = block.currentVelocity;
            setToyVelocityPortID = block.setToyVelocityPort.GetInstanceID();
            signalPortID = block.signalPort.GetInstanceID();
        }
        
    }
}