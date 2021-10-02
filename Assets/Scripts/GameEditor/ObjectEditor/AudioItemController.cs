using UnityEngine;
using UnityEngine.UI;

public class AudioItemController : MonoBehaviour {

    [SerializeField] private Text typeText;
    [SerializeField] private Text titleText;
    private AudioSource audioSource;

    AudioData _audioData;
    private void Awake() {
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }
    public void SetAudioData(AudioData data)
    {
        _audioData = data;
        RefreshUI();
    }  
    public void RefreshUI()
    {
        typeText.text = _audioData.GetType();
        titleText.text = System.IO.Path.GetFileName(_audioData.GetPath());
    } 

    public void OnButtonClicked()
    {
        audioSource.Stop();
        audioSource.clip = _audioData.GetAudioClip();
        audioSource.Play();
    }
}