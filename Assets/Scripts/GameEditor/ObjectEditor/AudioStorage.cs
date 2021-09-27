using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AudioData
{
    private string _path;
    public AudioData(string path)
    {
        _path = path;
    }

    public string getPath()
    {
        return _path;
    }
}

public class AudioStorage : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private AudioEditorController _audioEditorController;

    private void Awake()
    {
        _audioClips = new List<AudioClip>();
    }

    public IEnumerator GetAudioClip(AudioData data)
    {
        string path = data.getPath();
        string fileExtension = Path.GetExtension(path);
        AudioClip audioClip = null;
        
        if (fileExtension == ".wav")
        {
            Debug.Log(new System.Uri(path).AbsoluteUri);
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(
                new System.Uri(path).AbsoluteUri, AudioType.WAV
            ))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Error Occured : "+www.result);
                }
                else
                {
                    audioClip = DownloadHandlerAudioClip.GetContent(www);
                }
            }
        }
        else if (fileExtension == ".mp3")
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(
                new System.Uri(path).AbsoluteUri, AudioType.MPEG
            ))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Error Occured : "+www.result);
                }
                else
                {
                    audioClip = DownloadHandlerAudioClip.GetContent(www);
                }
            }
        }

        if (audioClip != null)
        {
            audioClip.name = Path.GetFileName(path);
            _audioClips.Add(audioClip);
        }
    }

    public void AddAudioData(AudioData data)
    {
        Debug.Log("test");
        StartCoroutine(GetAudioClip(data));
    }
}
