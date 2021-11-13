using SandboxEditor.Data.Block;
using SandboxEditor.Data.Block.Register;
using SandboxEditor.Data.Storage;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.Block
{
    public class AudioBlock : AbstractBlock
    {
        public AudioSource audioSource;
        public BlockPort playSignal;
        public BlockPort stopSignal;
        public string audioName;

        protected override void InitializePortRegister()
        {
            playSignal.register = new BoolRegister();
            stopSignal.register = new BoolRegister();
        }

        public override void OnEveryFixedUpdateWhenPlaying()
        {
            Debug.Log(stopSignal.register);
            if (stopSignal.RegisterValue != null && (bool)stopSignal.RegisterValue)
            {
                audioSource.Stop();
                return;
            }
            if (stopSignal.RegisterValue != null &&(bool)playSignal.RegisterValue)
                audioSource.Play();
        }
        
        

        public override BlockData SaveBlockData()
        {
            return new AudioBlockData(this);
        }
        
        public override void LoadBlockData(BlockData AudioData)
        {
            base.LoadBlockData(AudioData);
            audioName = ((AudioBlockData) AudioData).audioName;
            if (audioName != null)
                audioSource.clip = AudioStorage.audioStorage._audiosData[audioName].audioClip;
        }
    }
}