using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SandboxEditor.Block;
using SandboxEditor.Data.Resource;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.UI.Panel.Audio;
using UnityEngine;
using UnityEngine.Networking;
using File = Tools.File;

// 오디오를 메모리에 저장하고, 재생하기 위한 저장소입니다.
namespace SandboxEditor.Data.Storage
{
    public class AudioStorage : MonoBehaviour
    {
        // 오디오 데이터에 대한 리스트입니다.
        public Dictionary<string, AudioData> _audiosData;

        // Audio 편집기의 UI와 저장 상황을 동기화하기 위해 추가한 변수입니다.
        [SerializeField] private AudioEditorController _audioEditorController;
        public Sandbox.Sandbox sandbox;
        
        public static AudioStorage audioStorage;

        public AudioBlock selectedAudioBlock;
        public GameObject audioSelectPanel;
        private void Awake()
        {
            _audiosData = new Dictionary<string, AudioData>();
            LoadAudioClipResources(Resources.LoadAll<AudioClip>(File.BackgroundMusicPath), "BGM");
            LoadAudioClipResources(Resources.LoadAll<AudioClip>(File.EffectSoundPath), "EffectSound");
            _audioEditorController.RefreshUI(_audiosData.Values.ToList());

            audioStorage = this;
        }

        private void LoadAudioClipResources(IEnumerable<AudioClip> audiosData, string type)
        {
            foreach (var clip in audiosData)
            {
                var audioData = new AudioData(clip.name, type)
                {
                    audioClip = clip
                };
                _audiosData.Add(clip.name, audioData);
            }
        }

        // 코루틴 형태로 오디오 클립을 불러옵니다.
        private IEnumerator LoadAudioClip(AudioData audioData)
        {
            var fileExtension = Path.GetExtension(audioData.fileName);
            Debug.Log(audioData.fileName);
            Debug.Log(fileExtension);
            Debug.Log(new System.Uri(SandboxChecker.MakeFullPath(sandbox, audioData.fileName)).AbsoluteUri);
            AudioClip audioClip = null;
            var TypePair = new Dictionary<string, AudioType>()
            {
                {".wav", AudioType.WAV},
                {".mp3", AudioType.MPEG}
            };
            if (TypePair.ContainsKey(fileExtension))
            {
                Debug.Log(TypePair[fileExtension]);
                using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip 
                         (new Uri(SandboxChecker.MakeFullPath(sandbox, audioData.fileName)).AbsoluteUri, TypePair[fileExtension]))
                {
                    yield return www.SendWebRequest();
                    if (www.result != UnityWebRequest.Result.Success)
                        Debug.Log("Error Occured : "+www.result);
                    else
                        audioClip = DownloadHandlerAudioClip.GetContent(www);
                }
            }

            if (audioClip == null) yield break;
            audioClip.name = audioData.fileName;
            audioData.audioClip = audioClip;
            _audiosData.Add(audioData.fileName, audioData);
            _audioEditorController.RefreshUI(_audiosData.Values.ToList());
        }

        public void AddAudioData(AudioData data)
        {
            StartCoroutine(LoadAudioClip(data));
        }

    }
    [Serializable]
    public class AudiosData
    {
        public List<AudioData> audiosData;

        public AudiosData(List<AudioData> audiosData)
        {
            this.audiosData = audiosData;
        }
    }
}
