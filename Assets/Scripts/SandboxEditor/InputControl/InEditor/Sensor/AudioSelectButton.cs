using SandboxEditor.Data.Storage;
using SandboxEditor.NewBlock;
using SandboxEditor.UI.Panel.Audio;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class AudioSelectButton : AbstractSensor
    {
        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            isRayBlock = true;
            var audioStorage = AudioStorage.audioStorage;
            var audioBlock = GetComponentInParent<AudioBlock>();
            audioStorage.selectedAudioBlock = audioBlock;
            audioStorage.audioSelectPanel.SetActive(true);
            
        }
    }
}