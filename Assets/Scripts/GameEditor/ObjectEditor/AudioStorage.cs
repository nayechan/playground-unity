using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AudioStorage : MonoBehaviour
{
    [SerializeField] private List<AudioData> _audioDatas;
    [SerializeField] private AudioEditorController _audioEditorController;

    private void Awake()
    {
        _audioDatas = new List<AudioData>();
    }

    public IEnumerator LoadAudioClip(AudioData data)
    {
        CopyAudioData(data);

        string path = data.GetPath();
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
            Debug.Log("pass");
            audioClip.name = Path.GetFileName(path);
            data.SetAudioClip(audioClip);
            _audioDatas.Add(data);
            _audioEditorController.RefreshUI(_audioDatas);
        }
    }

    // 오디오 데이터를 앱 내부 데이터 폴더로 복사합니다.
    public void CopyAudioData(AudioData data)
    {
        string newPath = Application.persistentDataPath;
        newPath += ("/" + System.IO.Path.GetFileName(data.GetPath()));

        Debug.Log(newPath);

        System.IO.File.Copy(data.GetPath(), newPath, true);
        data.SetPath(newPath);
    }

    public void AddAudioData(AudioData data)
    {
        Debug.Log("test");
        StartCoroutine(LoadAudioClip(data));
    }
}
