using System;
using SandboxEditor.NewBlock;

namespace SandboxEditor.Data.Block
{
    [Serializable]
    public class VelocitySetterBlockData : BlockData
    {
        public float XVelocity;
        public float YVelocity;

        public VelocitySetterBlockData(VelocitySetterBlock block)
        {
            SetGameObjectIDAndPosition(block);
            XVelocity = block.XVelocity;
            YVelocity = block.YVelocity;
        }
        
    }
}