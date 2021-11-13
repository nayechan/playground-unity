using System;
using SandboxEditor.Block;

namespace SandboxEditor.Data.Block
{
    [Serializable]
    public class VelocitySetterBlockData : BlockData
    {
        public float XVelocity;
        public float YVelocity;

        public VelocitySetterBlockData(VelocitySetterBlock blockToSave) : base(blockToSave)
        {
            XVelocity = blockToSave.XVelocity;
            YVelocity = blockToSave.YVelocity;
        }
        
    }
}