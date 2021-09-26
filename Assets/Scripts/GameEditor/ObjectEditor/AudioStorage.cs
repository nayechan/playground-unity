using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioStorage : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private AudioClip audioClip;
    
    private void Start() {
        
    }

    public AudioClip GetAudioClip()
    {
        return audioClip;
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        this.audioClip = audioClip;
        audioSource.clip = audioClip;
    }
}
