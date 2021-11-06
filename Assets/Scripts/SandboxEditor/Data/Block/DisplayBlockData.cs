using System;
using SandboxEditor.NewBlock;

namespace SandboxEditor.Data.Block
{
    [Serializable]
    public class DisplayBlockData : BlockData
    {
        public float camSize;

        public DisplayBlockData(DisplayBlock blockToSave) : base(blockToSave)
        {
            camSize = blockToSave.camera.orthographicSize;
        }
    }
}