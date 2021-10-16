using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameEditor;
using UnityEngine;
using UnityEngine.Networking;

// 오디오를 메모리에 저장하고, 재생하기 위한 저장소입니다.
public class AudioStorage : MonoBehaviour
{
    // 오디오 데이터에 대한 리스트입니다.
    [SerializeField] private List<AudioData> _audioDatas;

    // Audio 편집기의 UI와 저장 상황을 동기화하기 위해 추가한 변수입니다.
    [SerializeField] private AudioEditorController _audioEditorController;
    public Sandbox sandbox;

    private void Awake()
    {
        _audioDatas = new List<AudioData>();
    }

    // 코루틴 형태로 오디오 클립을 불러옵니다.
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
        var fileName = System.IO.Path.GetFileName(data.GetPath());
        string newPath = SandboxChecker.MakeFullPath(sandbox, fileName);
        Debug.Log(newPath);
        System.IO.File.Copy(data.GetPath(), newPath, true);
        data.SetRelativePath(fileName);
    }

    public void AddAudioData(AudioData data)
    {
        Debug.Log("test");
        StartCoroutine(LoadAudioClip(data));
    }

    public void Test()
    {
        AudioSource audioSource;
        
    }
}
