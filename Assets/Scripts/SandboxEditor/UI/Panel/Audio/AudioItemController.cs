using GameEditor.Data;
using UnityEngine;
using UnityEngine.UI;

/*
AudioEditor에 표시될 각각의 Audio 선택요소를 관리하는 스크립트입니다.
*/
namespace GameEditor.Resource.Audio
{
    public class AudioItemController : MonoBehaviour {

        //타입
        [SerializeField] private Text typeText;

        //제목
        [SerializeField] private Text titleText;

        //재생하기 위한 AudioSource
        private AudioSource audioSource;

        AudioData _audioData;

        //Prefab 형태이기 때문에, 인스펙터에서 추가하는 형식으로는 AudioSource에 대한 선택이 불가능하여 다음과 같이 구현하였습니다.
        private void Awake() {
            audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        }

        // 이 요소에 대응하는 AudioData를 설정합니다.
        public void SetAudioData(AudioData data)
        {
            _audioData = data;
            RefreshUI();
        }  

        // 이 요소에 대한 UI 부분을 리프레시합니다.
        public void RefreshUI()
        {
            Debug.Log(typeText);
            typeText.text = _audioData.GetAudioType();
            titleText.text = System.IO.Path.GetFileName(_audioData.GetPath());
        } 

        // 이 요소가 클릭하였을 때의 동작입니다.
        public void OnButtonClicked()
        {
            audioSource.Stop();
            audioSource.clip = _audioData.GetAudioClip();
            audioSource.Play();
        }
    }
}