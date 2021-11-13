using SandboxEditor.Block;

namespace SandboxEditor.Data.Block
{
    public class AudioBlockData : BlockData
    {
        public string audioName;
        public AudioBlockData(AudioBlock blockToSave) : base(blockToSave)
        {
            audioName = blockToSave.audioName;
        }
    }
}